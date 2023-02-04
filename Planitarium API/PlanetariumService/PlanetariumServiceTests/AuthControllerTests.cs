using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using PlanetariumService.Controllers;
using PlanetariumService.Models;
using PlanetariumServices;
using PlanetariumModels;
using System.Threading.Tasks;
using NSubstitute.ReturnsExtensions;

namespace PlanetariumServiceTests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private IConfiguration configuration;
        private IUsersService userService;
        private ILogger<PlanetariumService.Controllers.AuthController> log;
        private AuthController authController;

        [SetUp]
        public void SetUp()
        {
            configuration = Substitute.For<IConfiguration>();
            userService = Substitute.For<IUsersService>();
            log = Substitute.For<ILogger<AuthController>>();

            authController = new AuthController(configuration, userService, log);
        }

        [Test]
        public void GetMeTest()
        {
            var response = authController.GetMe();

            Assert.IsNotNull(response);
            Assert.AreEqual(typeof(ActionResult<string>), response.GetType());
        }

        [Test]
        public void RegisterSuccessTest()
        {
            string Email = "mail@mail.com", Username = "User", UserPassword = "1234";
            var guid = new RegisterUI { Email = Email, Username = Username, UserPassword = UserPassword };
            var user = new Users { Id = 0, Email = Email, Username = Username, UserPassword = UserPassword };
            var newuser = new Users { Id = 1, Email = Email, Username = Username, UserPassword = UserPassword };
            userService.GetByUsername(guid.Username).ReturnsNull();
            userService.GetByEmail(guid.Email).ReturnsNull();
            userService.Add(Arg.Is<Users>(x => x.Id == user.Id && x.Email.Equals(user.Email) && x.UserPassword.Equals(user.UserPassword))).Returns(newuser);
            var response = authController.Register(guid);
            userService.ReceivedWithAnyArgs().GetByUsername(guid.Username);
            userService.ReceivedWithAnyArgs().GetByEmail(guid.Email);
            log.ReceivedWithAnyArgs().LogInformation("New user added");

            Assert.AreEqual(typeof(OkObjectResult), response.GetType());
            Task<Users> task = (Task<Users>) ((OkObjectResult)response).Value;
            Assert.AreEqual(typeof(Task<Users>), task.GetType());
            Assert.AreEqual(newuser, task.Result);
        }

        [Test]
        public void RegisterFailUsernameTest()
        {
            string Email = "mail@mail.com", Username = "User", UserPassword = "1234";
            var guid = new RegisterUI { Email = Email, Username = Username, UserPassword = UserPassword };
            userService.GetByUsername(guid.Username).Returns(new Users { Id = 0});
            userService.GetByEmail(guid.Email).ReturnsNull();
            var response = authController.Register(guid);
            userService.ReceivedWithAnyArgs().GetByUsername(guid.Username);
            userService.ReceivedWithAnyArgs().GetByEmail(guid.Email);
            log.ReceivedWithAnyArgs().LogError("Registration failed");

            Assert.AreEqual(typeof(BadRequestObjectResult), response.GetType());
            string task = (string) ((BadRequestObjectResult)response).Value;
            Assert.AreEqual(typeof(string), task.GetType());
            Assert.AreEqual("User already exists.", task);
        }

        [Test]
        public void RegisterFailEmailTest()
        {
            string Email = "mail@mail.com", Username = "User", UserPassword = "1234";
            var guid = new RegisterUI { Email = Email, Username = Username, UserPassword = UserPassword };
            userService.GetByUsername(guid.Username).ReturnsNull();
            userService.GetByEmail(guid.Email).Returns(new Users { Id = 0 });
            var response = authController.Register(guid);
            userService.ReceivedWithAnyArgs().GetByUsername(guid.Username);
            userService.ReceivedWithAnyArgs().GetByEmail(guid.Email);
            log.ReceivedWithAnyArgs().LogError("Registration failed");

            Assert.AreEqual(typeof(BadRequestObjectResult), response.GetType());
            string task = (string) ((BadRequestObjectResult)response).Value;
            Assert.AreEqual(typeof(string), task.GetType());
            Assert.AreEqual("User already exists.", task);
        }

        [Test]
        public void LoginUsernameTest()
        {
            string Login = "User", Password = "1234";
            var user = new Users { Id = 0, Username = Login, UserPassword = Password };
            var guid = new LoginUI { Login = Login, Password = Password };
            userService.GetByUsername(Login).Returns(user);

            var response = authController.Login(guid);

            userService.ReceivedWithAnyArgs().GetByUsername(Login);

            log.ReceivedWithAnyArgs().LogInformation("Login was successful");

            Assert.AreEqual(typeof(OkObjectResult), response.GetType());
            Users result = (Users) ((OkObjectResult)response).Value;
            Assert.AreEqual(typeof(Users), result.GetType());
            Assert.AreEqual(user, result);
        }

        [Test]
        public void LoginEmailTest()
        {
            string Login = "User", Password = "1234";
            var user = new Users { Id = 0, Email = Login, UserPassword = Password };
            var guid = new LoginUI { Login = Login, Password = Password };
            userService.GetByUsername(Login).ReturnsNull();
            userService.GetByEmail(Login).Returns(user);

            var response = authController.Login(guid);

            userService.ReceivedWithAnyArgs().GetByUsername(Login);
            userService.ReceivedWithAnyArgs().GetByEmail(Login);

            log.ReceivedWithAnyArgs().LogInformation("Login was successful");

            Assert.AreEqual(typeof(OkObjectResult), response.GetType());
            Users result = (Users) ((OkObjectResult)response).Value;
            Assert.AreEqual(typeof(Users), result.GetType());
            Assert.AreEqual(user, result);
        }

        [Test]
        public void LoginFailTest()
        {
            string Login = "User", Password = "1234";
            var guid = new LoginUI { Login = Login, Password = "1111" };
            var user = new Users { Id = 0, Email = Login, UserPassword = Password };
            userService.GetByUsername(Login).ReturnsNull();
            userService.GetByEmail(Login).Returns(user);

            var response = authController.Login(guid);

            userService.ReceivedWithAnyArgs().GetByUsername(Login);
            userService.ReceivedWithAnyArgs().GetByEmail(Login);

            log.ReceivedWithAnyArgs().LogError("Wrong password");

            Assert.AreEqual(typeof(BadRequestObjectResult), response.GetType());
            string task = (string) ((BadRequestObjectResult)response).Value;
            Assert.AreEqual(typeof(string), task.GetType());
            Assert.AreEqual("Wrong password.", task);
        }

        [Test]
        public void LoginFailPasswordTest()
        {
            string Login = "User", Password = "1234";
            var guid = new LoginUI { Login = Login, Password = Password };
            userService.GetByUsername(Login).ReturnsNull();
            userService.GetByEmail(Login).ReturnsNull();

            var response = authController.Login(guid);

            userService.ReceivedWithAnyArgs().GetByUsername(Login);
            userService.ReceivedWithAnyArgs().GetByEmail(Login);

            log.ReceivedWithAnyArgs().LogError("Not found");

            Assert.AreEqual(typeof(BadRequestObjectResult), response.GetType());
            string task = (string) ((BadRequestObjectResult)response).Value;
            Assert.AreEqual(typeof(string), task.GetType());
            Assert.AreEqual("User not found.", task);
        }

        [Test]
        public void LogoutTest()
        {
            var response = authController.Logout();

            log.ReceivedWithAnyArgs().LogInformation("Logged out");

            Assert.AreEqual(typeof(OkObjectResult), response.GetType());
            string result = (string) ((OkObjectResult)response).Value;
            Assert.AreEqual(typeof(string), result.GetType());
            Assert.AreEqual("Logged out", result);
        }

            /*[Test]
            public void RegisterTest()
            {
                var guid = new Users { Id = 2 };
                userService.Add(guid).Returns(guid);

                Users result = userService.Add(guid).Result;

                Assert.IsNotNull(result);
                Assert.AreEqual(guid, result);
                Assert.AreEqual(2, result.Id);
            }

            [Test]
            public void LoginTest()
            {
                var guid = new Users { Id = 2 };
                var response = authController.Login(guid);

                Assert.IsNotNull(response);
                Assert.AreEqual(typeof(Task<ActionResult<string>>), response.GetType());
            }*/

        }
}

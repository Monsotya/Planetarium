using NSubstitute;
using NUnit.Framework;
using PlanetariumServices;
using PlanetariumModels;
using Microsoft.EntityFrameworkCore;
using PlanetariumRepositories;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace PlanetariumServiceTests
{
    [TestFixture]
    public class UsersModelTests
    {
        private PlanetariumServiceContext Context;
        private UsersRepository repository;
        private IUsersService service;

        [SetUp]
        public void SetUp()
        {
            PlanetariumServiceContext.Connection = new PlanetariumModelsFramework.PlanetariumServiceContext().Database.Connection.ConnectionString;
            Context = new PlanetariumServiceContext(new DbContextOptions<PlanetariumServiceContext>());

            IHttpContextAccessor accessor = Substitute.For<IHttpContextAccessor>();
            repository = new UsersRepository(Context);
            service = new UsersService(accessor, repository);
        }

        [Test]
        public void AddUserTest()
        {
            int Id = default;
            Users user = new Users { Id = Id, Username = "TestUser", Email = "mail@m.mail", UserPassword = "1" };
            try
            {
                service.Add(user);
            } catch (System.Exception e)
            {
                Context.Users.Remove(user);
                Context.SaveChanges();
                service.Add(user);
            }
            Assert.AreNotEqual(Id, service.GetByUsername(user.Username).Id);
            Context.Users.Remove(user);
            Context.SaveChanges();
        }

        [Test]
        public void AddUserGetTest()
        {
            Users user = new Users { Id = default, Username = "TestUser", Email = "mail@m.mail", UserPassword = "1" };
            try
            {
                service.Add(user);
            }
            catch (System.Exception e)
            {
                Context.Users.Remove(user);
                Context.SaveChanges();
                service.Add(user);
            }

            Users result = service.GetByUsername(user.Username);
            Assert.AreEqual(user.Username, result.Username);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.UserPassword, result.UserPassword);
            result = service.GetByEmail(user.Email);
            Assert.AreEqual(user.Username, result.Username);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.UserPassword, result.UserPassword);
            Context.Users.Remove(user);
            Context.SaveChanges();
        }

        [Test]
        public void AddUniqueTest()
        {
            Users user1 = new Users { Id = default, Username = "TestUser", Email = "mail@m.mail", UserPassword = "1" };
            Users user2 = new Users { Id = default, Username = "TestUser", Email = "email@mail.mail", UserPassword = "2" };
            Users user3 = new Users { Id = default, Username = "TestUserNew", Email = "mail@m.mail", UserPassword = "3" };
            try
            {
                service.Add(user1);
            }
            catch (System.Exception e)
            {
                Context.Users.Remove(user1);
                Context.SaveChanges();
                Context.Users.Add(user1);
                Context.SaveChanges();
            }
            if (service.GetByEmail(user2.Email) != null) 
            { 
                Context.Users.Remove(user2);
                Context.SaveChanges();
            }
            if (service.GetByUsername(user3.Username) != null)
            {
                Context.Users.Remove(user3);
                Context.SaveChanges();
            }
            Assert.Throws<DbUpdateException>(() => {
                service.Add(user2);
                Context.SaveChanges();
            });

            //Users result;
            //result = service.GetByUsername(user1.Username);
            /*Assert.AreEqual(user1.Username, result.Username);
            Assert.AreEqual(user1.Email, result.Email);
            Assert.AreEqual(user1.UserPassword, result.UserPassword);*/

            Assert.Throws<DbUpdateException>(() => {
                service.Add(user3);
                Context.SaveChanges();
            });

            //result = service.GetByUsername(user1.Username);
            /*Assert.AreEqual(user1.Username, result.Username);
            Assert.AreEqual(user1.Email, result.Email);
            Assert.AreEqual(user1.UserPassword, result.UserPassword);*/
            //Assert.Throws<SqlException>(() => service.Add(user2));
            //Assert.Throws<SqlException>(() => service.Add(user3));
            Context.Users.Remove(user1);
            Context.SaveChanges();
            if (service.GetByUsername(user2.Username) != null)
            {
                Context.Users.Remove(user2);
                Context.SaveChanges();
            }
            if (service.GetByUsername(user3.Username) != null)
            {
                Context.Users.Remove(user3);
                Context.SaveChanges();
            }
        }
    }
}
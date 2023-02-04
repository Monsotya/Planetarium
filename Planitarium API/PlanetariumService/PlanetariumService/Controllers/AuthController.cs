using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlanetariumModels;
using PlanetariumService.Models;
using PlanetariumServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PlanetariumService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static UserUI user = new UserUI();
        private readonly IConfiguration configuration;
        private readonly IUsersService userService;
        private readonly ILogger<AuthController> log;

        public AuthController(IConfiguration configuration, IUsersService userService, ILogger<AuthController> log)
        {
            this.log = log;
            this.configuration = configuration;
            this.userService = userService;
        }

        /// <summary>
        /// Gets user`s name
        /// </summary>
        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {            
            try
            {
                var userName = userService.GetMyName();
                log.LogInformation("Got user`s name");
                return Ok(userName);
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Something went wrong");
                throw;
            }
        }

        /// <summary>
        /// Login for user
        /// </summary>
        [HttpPost("Signin")]
        public IActionResult Login(LoginUI model)
        {
            if (ModelState.IsValid)
            {
                var user = userService.GetByUsername(model.Login);
                if (user == null)
                {
                    user = userService.GetByEmail(model.Login);
                    if (user == null)
                    {
                        log.LogError("Not found");
                        return BadRequest("User not found.");
                    }
                }
                if (user.UserPassword != model.Password)
                {
                    log.LogError("Wrong password");
                    return BadRequest("Wrong password.");
                }
                log.LogInformation("Login was successful");

                return Ok(user);
            }
            return BadRequest("Wrong state.");
        }

        /// <summary>
        /// Registration for new user
        /// </summary>
        [HttpPost("Signup")]
        public IActionResult Register(RegisterUI model) 
        {
            if (ModelState.IsValid)
            {
                bool error = false;
                if (userService.GetByUsername(model.Username) != null)
                {
                    error = true;
                }
                if (userService.GetByEmail(model.Email) != null)
                {
                    error = true;
                }

                if (error)
                {
                    log.LogError("Registration failed");
                    return BadRequest("User already exists.");
                }

                Users user = new Users
                {
                    Username = model.Username,
                    Email = model.Email,
                    UserPassword = model.UserPassword

                };
                var res = userService.Add(user);
                log.LogInformation("New user added");
                return Ok(res);
            }
            return BadRequest("Wrong data");
        }

        public ActionResult Logout()
        {
            log.LogInformation("Logged out");
            return Ok("Logged out");
        }

        /*[HttpPost("register")]
        public async Task<ActionResult<UserUI>> Register(Users request)
        {
            user.Username = request.Username;
            user.Password = request.UserPassword;
            user.Role = request.UserRole;
            try
            {
                userService.Add(request);
                log.LogInformation("New user added");
                return Ok(user);
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Registration failed");
                throw;
            }            
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(Users request)
        {

            if (userService.GetAll().FirstOrDefault(x => x.Username == request.Username) == null)
            {
                log.LogError("Not found");
                return BadRequest("User not found.");
            }

            if (userService.GetAll().FirstOrDefault(x => x.Username == request.Username).UserPassword != request.UserPassword)
            {
                log.LogError("Wrong password");
                return BadRequest("Wrong password.");
            }

            user.Username = request.Username;
            user.Password = request.UserPassword;
            try
            {
                user.Role = userService.GetAll().FirstOrDefault(x => x.Username == request.Username).UserRole;
                string token = CreateToken(user);
                log.LogInformation("Login was successful");
                return Ok(token);
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Login failed");
                throw;
            }
            
        }*/

        private string CreateToken(UserUI user)
        {
            List<Claim> claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, user.Username),
                 new Claim(ClaimTypes.Role, user.Role)
             };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }

}


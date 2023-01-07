using Microsoft.AspNetCore.Mvc;
using PlanetariumModels;
using PlanetariumServices;
using Microsoft.AspNetCore.Authorization;

namespace PlanetariumService.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsersService userService;

        public AuthController(IUsersService userService)
        {
            this.userService = userService;
        }


        [Route("Signin")]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("Signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string login, string password) // LoginViewModel model
        {
            if (ModelState.IsValid)
            {
                // var userdetails = await _context.Userdetails
                // .SingleOrDefaultAsync(m => m.Email == model.Email && m.Password == model.Password);
                var user = userService.GetByUsername(login);
                if (user == null)
                {
                    ModelState.AddModelError("Login", "Invalid login.");
                    return RedirectToAction(nameof(SignIn));
                }
                if (user.UserPassword != password)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return RedirectToAction(nameof(SignIn));
                }
                HttpContext.Session.SetString("userId", user.Username);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Index");
            }
        }
        /*{
            // TODO: Insert login and password retrival from database
            // User user;
            var user = new { Username = "Me", Password = "1234", Role = "Admin" };
            if (user.Username == login && user.Password == password)
            {
                var identity = new GenericIdentity(login);
                HttpContext.User = new GenericPrincipal(identity, new[] { user.Role });
            }
            return View();
        }*/


        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users
                {
                    Username = model.Username,
                    *//*Name = model.Name,
                    Lastame = model.Lastname*//*
                    // Email = model.Email,
                    UserPassword = model.Password

                };
                var res = userService.Add(user);
                //HttpContext.Session.SetString("userId", user.Username);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }*/

        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(SignIn));
            // return RedirectToPage()
        }
    }
}
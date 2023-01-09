using Microsoft.AspNetCore.Mvc;
using PlanetariumModels;
using PlanetariumServices;
using Microsoft.AspNetCore.Authorization;
using PlanetariumService.Models;

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
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [Route("Signup")]
        public IActionResult SignUp()
        {
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // [Route("Signin/Login")]
        [Route("Signin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginUI model) // LoginViewModel model
        {
            if (ModelState.IsValid)
            {
                // var userdetails = await _context.Userdetails
                // .SingleOrDefaultAsync(m => m.Email == model.Email && m.Password == model.Password);
                /*Users user = null; 
                try {
                    user = userService.GetByUsername(model.Login);
                } catch (AggregateException e) {
                    //ModelState.AddModelError("Login", "Invalid Username.");
                    ViewBag.LoginError = "Invalid Username.";
                    return RedirectToAction(nameof(SignIn));
                }*/
                var user = userService.GetByUsername(model.Login);
                if (user == null)
                {
                    user = userService.GetByEmail(model.Login);
                    if (user == null)
                    {
                        TempData["LoginError"] = "Invalid Username or Email.";
                        return RedirectToAction(nameof(SignIn));
                    }
                    // ModelState.AddModelError("Login", "Invalid Username.");
                }
                if (user.UserPassword != model.Password)
                {
                    //ModelState.AddModelError("Password", "Invalid Sign in attempt.");
                    TempData["PasswordError"] = "Invalid Sign in attempt. (Wrong Password)";
                    return RedirectToAction(nameof(SignIn));
                }
                //HttpContext.Session.SetString("userName", user.Username);
                TempData["Alert"] = "Signed in successfully!";
                HttpContext.Session.SetInt32("userId", user.Id);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(SignIn));//View("Index");
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

        // [Route("Signup/Register")]
        [Route("Signup")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterUI model) {//(RegistrationViewModel model)
            if (ModelState.IsValid)
            {
                bool error = false;
                if (userService.GetByUsername(model.Username) != null)
                {
                    TempData["UsernameError"] = "Username already used.";
                    error = true;
                }
                if (userService.GetByEmail(model.Email) != null)
                {
                    TempData["EmailError"] = "Email already used.";
                    error = true;
                }

                if (error)
                {
                    return RedirectToAction(nameof(SignUp));
                }

                Users user = new Users
                {
                    Username = model.Username,
                    /*Name = model.Name,
                    Lastame = model.Lastname,*/
                    Email = model.Email,
                    UserPassword = model.UserPassword

                };
                var res = userService.Add(user);
                // HttpContext.Session = user.Username;
                //HttpContext.Session.SetString("userName", user.Username);
                TempData["Alert"] = "Registration complete. Signed in successfully!";
                HttpContext.Session.SetInt32("userId", user.Id);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(SignUp));//View();
        }

        //[Authorize]
        public ActionResult Logout()
        {
            if (HttpContext.Session.GetInt32("userId") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(SignIn));
            // return RedirectToPage()
        }
    }
}
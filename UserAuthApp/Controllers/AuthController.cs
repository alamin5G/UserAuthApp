using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using UserAuthApp.Data;
using UserAuthApp.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;

namespace UserAuthApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AuthController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET /auth/register returns the registration form
        [HttpGet]
        [Route("auth/register")]
        public IActionResult Register()
        {
            return View();
        }

        // POST /auth/register handles the registration form submission
        [HttpPost]
        [Route("auth/register")]
        public IActionResult Register(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                userModel.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userModel.PasswordHash);
                _appDbContext.Users.Add(userModel);
                _appDbContext.SaveChanges();
                TempData["success"] = "User registered successfully!";
                ViewBag.Message = "Registration Succeed!";
                //pass the username using session data
                if (userModel.Username != null)
                {
                    HttpContext.Session.SetString("Username", userModel.Username);
                }
                return RedirectToAction("Welcome");
                // return RedirectToAction("Login");
            }
            return View();
        }

        // GET /auth/login returns the login form
        [HttpGet]
        [Route("auth/login")]
        public IActionResult Login()
        {
            return View();
        }

        // POST /auth/login handles the login form submission
        [HttpPost]
        [Route("auth/login")]
        public IActionResult Login(UserModel userModel)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Username == userModel.Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(userModel.PasswordHash, user.PasswordHash))
            {
                ViewBag.Message = "Login successful!";
                TempData["success"] = "Login successful!";
                //TempData["username"] = user.Username;

                if (userModel.Username != null)
                {
                    HttpContext.Session.SetString("Username", userModel.Username);
                }

                

                    return RedirectToAction("Welcome");
            }

            ViewBag.Message = "Invalid username or password!";

            return View();
        }

        // GET /auth/logout logs out the user
        [HttpGet]
        [Route("auth/logout")]
        public IActionResult Logout()
        {
            TempData["success"] = "Logged out successfully!";
            HttpContext.Session.Remove("Username");

            return View("Welcome");
        }


        public IActionResult Welcome()
        {

            String? Username = HttpContext.Session.GetString("Username");

            if (Username != null)
            {
                TempData["username"] = Username;
                ViewBag.Message = $" {Username}";
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
                
        }
    }
}

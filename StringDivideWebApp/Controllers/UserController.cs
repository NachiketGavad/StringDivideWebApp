using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StringDivideWebApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace StringDivideWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;

        public UserController( ApplicationDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(StringDivideAppUser user)
        {
            if (ModelState.IsValid)
            {
                // Check if the user with the same email already exists
                var existingUser = _context.StringDivideAppUsers.FirstOrDefault(u => u.email == user.email);

                if (existingUser == null)
                {
                    // Hash the password before storing it in the database
                    user.PasswordHash = _passwordService.HashPassword(user.password);

                    // Add the user to the database
                    _context.StringDivideAppUsers.Add(user);
                    _context.SaveChanges();

                    return RedirectToAction("Login");
                }
                else
                {
                    // If the user already exists, return an error indicating duplication
                    ModelState.AddModelError("", "A user with the provided email already exists.");
                }
            }

            // If ModelState is not valid, return the view with validation errors
            return View(user);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(StringDivideAppUser user)
        {
            // Find the user by email
            var myuser = _context.StringDivideAppUsers.FirstOrDefault(x => x.email == user.email);

            if (myuser != null)
            {
                // Hash the provided password
                string hashedPassword = _passwordService.HashPassword(user.password);

                // Compare hashed passwords
                if (myuser.PasswordHash == hashedPassword)
                {
                    // Authentication successful
                    HttpContext.Session.SetString("UserSession", user.email);
                    return RedirectToAction("Index", "Home");
                }
            }

            // Invalid email or password
            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

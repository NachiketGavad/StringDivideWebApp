using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StringDivideWebApp.Models;

namespace StringDivideWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController( ApplicationDbContext context)
        {
            _context = context;
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
                    // If the user does not exist, add them to the database
                    _context.StringDivideAppUsers.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    // If the user already exists, return an error indicating duplication
                    ModelState.AddModelError("", "Error, Please check Email And Password");
                    return View();
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Login(StringDivideAppUser user)
        {
            // Where always not null
            //var myuser = _context.StringDivideAppUsers.Where(x=>x.email==user.email && x.password==user.password);
            var myuser = _context.StringDivideAppUsers.FirstOrDefault(x => x.email == user.email && x.password == user.password);

            if (myuser != null)
            {
                HttpContext.Session.SetString("UserSession", user.email);
                ViewBag.Mysession= HttpContext.Session.GetString("UserSession").ToString();
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return RedirectToAction("Login");
            }
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

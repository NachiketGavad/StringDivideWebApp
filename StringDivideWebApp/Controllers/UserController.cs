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
            if(ModelState.IsValid){
                _context.StringDivideAppUsers.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Login()
        {
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

        [HttpPost]
        public IActionResult Login(StringDivideAppUser user)
        {
            var myuser = _context.StringDivideAppUsers.Where(x=>x.email==user.email && x.password==user.password);
            if (myuser != null)
            {
                HttpContext.Session.SetString("UserSession", user.email);
                ViewBag.Mysession= HttpContext.Session.GetString("UserSession").ToString();
                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}

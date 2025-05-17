using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace E_Commerce_MVC.PL.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            // Replace with your actual admin credentials
            if (email == "admin@admin.com" && password == "AdminPassword123")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("IsLoggedIn", "true");
                return RedirectToAction("Index", "Product");
            }
            // For normal user login (example, you can expand this logic)
            if (email == "user@user.com" && password == "UserPassword123")
            {
                HttpContext.Session.SetString("IsLoggedIn", "true");
                HttpContext.Session.Remove("IsAdmin");
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Invalid credentials";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
} 
using E_Commerce_MVC.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce_MVC.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Create a list of sample products for the home page
            var products = new List<CartItem>
            {
                new CartItem { Id = 1, Name = "AK-900 Wired Keyboard", Price = 960, ImageUrl = "~/images/keyboard.jpg" },
                new CartItem { Id = 2, Name = "iPhone 14 Pro Max", Price = 999, ImageUrl = "~/images/keyboard.jpg" },
                new CartItem { Id = 3, Name = "MacBook Pro M2", Price = 1499, ImageUrl = "~/images/keyboard.jpg" },
                new CartItem { Id = 4, Name = "Apple Watch Series 8", Price = 399, ImageUrl = "~/images/keyboard.jpg" },
                new CartItem { Id = 5, Name = "AirPods Pro 2", Price = 199, ImageUrl = "~/images/keyboard.jpg" },
                new CartItem { Id = 6, Name = "iPad Pro 12.9\"", Price = 799, ImageUrl = "~/images/keyboard.jpg" },
                new CartItem { Id = 7, Name = "Sony A7IV Camera", Price = 2499, ImageUrl = "~/images/camera.jpg" },
                new CartItem { Id = 8, Name = "PlayStation 5", Price = 299, ImageUrl = "~/images/ps5.jpg" },
                new CartItem { Id = 9, Name = "Samsung 4K Monitor", Price = 449, ImageUrl = "~/images/monitor.jpg" },
                new CartItem { Id = 10, Name = "Sony WH-1000XM5", Price = 349, ImageUrl = "~/images/headphones.jpg" },
                new CartItem { Id = 11, Name = "Samsung Galaxy Tab S9", Price = 799, ImageUrl = "~/images/keyboard.jpg" },
                new CartItem { Id = 12, Name = "JBL PartyBox 310", Price = 399, ImageUrl = "~/images/speaker.jpg" }
            };

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

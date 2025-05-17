using Microsoft.AspNetCore.Mvc;
using E_Commerce.Services.Interfaces;
using E_Commerce.Services.Model_View;

namespace E_Commerce_MVC.PL.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProductService _productService;
        public ProfileController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                var products = await _productService.GetAllProductsAsync();
                return View("AdminDashboard", products);
            }
            // Normal user profile logic here
            return View();
        }
    }
} 
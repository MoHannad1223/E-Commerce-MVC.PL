using E_Commerce.Data.DataOrEntity;
using E_Commerce.Services.Interfaces;
using E_Commerce.Services.Model_View;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync(); // الآن بيرجع List<ProductVM>
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Index", "Login");
            ViewBag.Brands = await _productService.GetAllBrandsAsync();
            ViewBag.Types = await _productService.GetAllTypesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM model)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Index", "Login");
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = await _productService.GetAllBrandsAsync();
                ViewBag.Types = await _productService.GetAllTypesAsync();
                return View(model);
            }

            await _productService.AddProductAsync(model);
            TempData["Success"] = "Product created successfully.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Index", "Login");
            if (id <= 0)
                return BadRequest("Invalid product Id");

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound();

            var viewModel = new UpdateProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                StockQuantity = product.StockQuantity
            };

            ViewBag.Brands = await _productService.GetAllBrandsAsync();
            ViewBag.Types = await _productService.GetAllTypesAsync();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductVM model)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Index", "Login");
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = await _productService.GetAllBrandsAsync();
                ViewBag.Types = await _productService.GetAllTypesAsync();
                return View(model);
            }

            try
            {
                await _productService.UpdateProductAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            ViewBag.Brands = await _productService.GetAllBrandsAsync();
            ViewBag.Types = await _productService.GetAllTypesAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Index", "Login");
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product); // ProductVM
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
                return RedirectToAction("Index", "Login");
            await _productService.DeleteProductAsync(id);
            TempData["Success"] = "Product deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}

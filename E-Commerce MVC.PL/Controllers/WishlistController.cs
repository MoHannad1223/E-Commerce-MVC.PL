using Microsoft.AspNetCore.Mvc;
using E_Commerce_MVC.PL.Models;
using System.Text.Json;

namespace E_Commerce_MVC.PL.Controllers
{
    public class WishlistController : Controller
    {
        private const string WishlistSessionKey = "WishlistItems";

        public IActionResult Index()
        {
            var wishlistItems = GetWishlistItems();
            return View(wishlistItems);
        }

        [HttpPost]
        public IActionResult AddToWishlist([FromForm] CartItem item)
        {
            var wishlistItems = GetWishlistItems();
            if (!wishlistItems.Any(i => i.Id == item.Id))
            {
                wishlistItems.Add(item);
                SaveWishlistItems(wishlistItems);
            }
            return Json(new { success = true, message = "Product added to wishlist!" });
        }

        [HttpPost]
        public IActionResult RemoveFromWishlist(int id)
        {
            var wishlistItems = GetWishlistItems();
            var itemToRemove = wishlistItems.FirstOrDefault(i => i.Id == id);
            if (itemToRemove != null)
            {
                wishlistItems.Remove(itemToRemove);
                SaveWishlistItems(wishlistItems);
                return Json(new { success = true, message = "Product removed from wishlist!" });
            }
            return Json(new { success = false, message = "Product not found in wishlist." });
        }

        private List<CartItem> GetWishlistItems()
        {
            var wishlistJson = HttpContext.Session.GetString(WishlistSessionKey);
            return wishlistJson == null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(wishlistJson);
        }

        private void SaveWishlistItems(List<CartItem> wishlistItems)
        {
            var wishlistJson = JsonSerializer.Serialize(wishlistItems);
            HttpContext.Session.SetString(WishlistSessionKey, wishlistJson);
        }
    }
} 
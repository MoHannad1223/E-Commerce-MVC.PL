using Microsoft.AspNetCore.Mvc;
using E_Commerce_MVC.PL.Models;
using System.Text.Json;

namespace E_Commerce_MVC.PL.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "CartItems";

        public IActionResult Index()
        {
            var cartItems = GetCartItems();
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(CartItem item)
        {
            var cartItems = GetCartItems();
            
            var existingItem = cartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cartItems.Add(item);
            }

            SaveCartItems(cartItems);
            return Json(new { success = true, message = "Product added to cart successfully!" });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cartItems = GetCartItems();
            var itemToRemove = cartItems.FirstOrDefault(i => i.Id == id);
            
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                SaveCartItems(cartItems);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var cartItems = GetCartItems();
            var item = cartItems.FirstOrDefault(i => i.Id == id);
            
            if (item != null)
            {
                item.Quantity = quantity;
                SaveCartItems(cartItems);
            }

            return RedirectToAction("Index");
        }

        private List<CartItem> GetCartItems()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            return cartJson == null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        private void SaveCartItems(List<CartItem> cartItems)
        {
            var cartJson = JsonSerializer.Serialize(cartItems);
            HttpContext.Session.SetString(CartSessionKey, cartJson);
        }
    }
} 
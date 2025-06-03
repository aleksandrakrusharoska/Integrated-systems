using LibraryService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LibraryDomain.Domain;
using LibraryDomain.DTOs;
using LibraryDomain.Relationship;

namespace LibraryWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) throw new Exception($"User not found");

            var shoppingCart = _shoppingCartService.GetByOwnerIncludeBooks(userId);

            if (shoppingCart == null) throw new Exception($"Shopping cart not found for user {userId}");

            var booksInShoppingCart = shoppingCart.BooksInShoppingCart?.ToList() ?? new List<BooksInShoppingCart>();

            var viewModel = new ShoppingCartDto
            {
                BooksInShoppingCart = booksInShoppingCart,
                TotalPrice = booksInShoppingCart.Sum(b => (b.Book?.Price ?? 0) * b.Quantity)
            };

            return View(viewModel);
        }

        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) throw new Exception($"User not found");


            _shoppingCartService.OrderShoppingCart(userId);

            return RedirectToAction(nameof(Index));
        }
    }
}

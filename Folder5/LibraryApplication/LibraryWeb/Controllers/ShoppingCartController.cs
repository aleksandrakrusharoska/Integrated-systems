using LibraryService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LibraryDomain.Domain;
using LibraryDomain.DTOs;
using LibraryDomain.Integrations;
using LibraryDomain.Relationship;
using Microsoft.Extensions.Options;
using Stripe;

namespace LibraryWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly StripeSettings _stripeSettings;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IOptions<StripeSettings> stripeSettings)
        {
            _shoppingCartService = shoppingCartService;
            _stripeSettings = stripeSettings.Value;
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

        [HttpPost]
        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null) throw new Exception($"User not found");

                var shoppingCart = _shoppingCartService.GetByOwnerIncludeBooks(userId);
                if (shoppingCart == null) throw new Exception($"Shopping cart not found for user {userId}");

                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
                var customerService = new CustomerService();
                var customer = customerService.Create(new CustomerCreateOptions()
                {
                    Email = stripeEmail,
                    Source = stripeToken,
                });
                var chargeService = new ChargeService();
                var charge = chargeService.Create(new ChargeCreateOptions()
                {
                    Amount = Convert.ToInt32(shoppingCart.BooksInShoppingCart.Sum(b => (b.Book?.Price ?? 0) * b.Quantity)),
                    Description = "Library App Payment",
                    Currency = "usd",
                    Customer = customer.Id,
                });

                if (charge.Status == "succeeded")
                {
                    _shoppingCartService.OrderShoppingCart(userId);
                    return RedirectToAction("SuccessPayment");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return RedirectToAction("UnsuccessfulPayment");
        }

        public IActionResult UnsuccessfulPayment()
        {
            return View();
        }

        public IActionResult SuccessPayment()
        {
            return View();
        }
    }
}

using LibraryDomain.Domain;
using LibraryDomain.DTOs;
using LibraryDomain.Relationship;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using LibraryService.Interfaces;
using LibraryWeb.Data;

namespace LibraryWeb.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService bookService;
        private readonly IShoppingCartService shoppingCartService;

        public BooksController(IBookService bookService, IShoppingCartService shoppingCartService)
        {
            this.bookService = bookService;
            this.shoppingCartService = shoppingCartService;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(bookService.GetAll());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetById(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,Id,CreatedOn,Image,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = Guid.NewGuid();
                bookService.Create(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetById(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Author,Id,CreatedOn,Image,Price")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bookService.Update(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = bookService.GetById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            bookService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // Get: Books/AddBookToCart/5
        [HttpGet]
        public async Task<IActionResult> AddBookToCart(Guid? id)
        {
            if (id == null) return RedirectToAction(nameof(Index));

            var book = bookService.GetById(id.Value);

            if (book == null) return RedirectToAction(nameof(Index));

            return View(book);
        }


        // Post: Books/AddBookToCart/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBookToCart(AddBookToCartDto dto)
        {
            if (dto == null) return RedirectToAction(nameof(Index));

            var book = bookService.GetById(dto.BookId);
            if (book == null) return RedirectToAction(nameof(Index));

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return RedirectToAction(nameof(Index));

            var userCart = shoppingCartService.GetByOwner(userId);
            if (userCart == null) return RedirectToAction(nameof(Index));

            bookService.AddBookToCart(new BooksInShoppingCart
            {
                Book = book,
                BookId = book.Id,
                ShoppingCart = userCart,
                ShoppingCartId = userCart.Id,
                Quantity = dto.Quantity
            });

            return RedirectToAction(nameof(Index));
        }


        private bool BookExists(Guid id)
        {
            return bookService.GetById(id) != null;
        }
    }
}

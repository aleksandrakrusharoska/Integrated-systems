using LibraryDomain.Domain;
using LibraryDomain.Relationship;
using LibraryRepository.Interface;
using LibraryService.Interfaces;

namespace LibraryService.Implementations
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> bookRepository;
        private readonly IRepository<BooksInShoppingCart> booksInShoppingCartRepository;

        public BookService(IRepository<Book> bookRepository, IRepository<BooksInShoppingCart> booksInShoppingCartRepository)
        {
            this.bookRepository = bookRepository;
            this.booksInShoppingCartRepository = booksInShoppingCartRepository;
        }

        public Book Create(Book book)
        {
            return bookRepository.Create(book);
        }

        public Book Update(Book book)
        {
            return bookRepository.Update(book);
        }

        public Book Delete(Guid id)
        {
            var book = GetById(id);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            return bookRepository.Delete(book);
        }

        public Book? GetById(Guid id)
        {
            return bookRepository.Get(selector: book => book, filter: book => book.Id.Equals(id));
        }

        public List<Book> GetAll()
        {
            return bookRepository.GetAll(selector: book => book).ToList();
        }

        public void AddBookToCart(BooksInShoppingCart booksInShoppingCart)
        {
            var existingBookInShoppingCart = booksInShoppingCartRepository.Get(
                selector: b => b,
                filter: b => 
                    b.BookId.Equals(booksInShoppingCart.BookId)
                    && b.ShoppingCartId.Equals(booksInShoppingCart.ShoppingCartId));

            if (existingBookInShoppingCart != null)
            {
                existingBookInShoppingCart.Quantity += booksInShoppingCart.Quantity;
                booksInShoppingCartRepository.Update(existingBookInShoppingCart);
            }
            else
            {
                booksInShoppingCartRepository.Create(booksInShoppingCart);
            }
        }
    }
}
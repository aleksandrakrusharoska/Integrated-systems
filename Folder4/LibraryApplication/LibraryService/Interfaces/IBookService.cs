using LibraryDomain.Domain;
using LibraryDomain.Relationship;

namespace LibraryService.Interfaces
{
    public interface IBookService
    {
        Book Create(Book book);

        Book Update(Book book);

        Book Delete(Guid id);

        Book? GetById(Guid id);

        List<Book> GetAll();

        void AddBookToCart(BooksInShoppingCart booksInShoppingCart);
    }
}
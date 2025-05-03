using Chas1.Interface;

namespace Chas1.Implementation
{
    public class Library : ILibraryInterface
    {
        public Book? GetMostBorrowedBook(List<Book> books)
        {
            return books.OrderByDescending(b => b.BorrowedCopies).FirstOrDefault();
        }
    }
}

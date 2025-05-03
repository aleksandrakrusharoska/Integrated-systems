using ConsoleApp1.Interface;

namespace ConsoleApp1.Implementation
{
    public class Library : ILibraryInterface
    {
        public Book? GetMostBorrowedBook(List<Book> books)
        {
            return books.OrderByDescending(b => b.BorrowedCopies).FirstOrDefault();
        }
    }
}

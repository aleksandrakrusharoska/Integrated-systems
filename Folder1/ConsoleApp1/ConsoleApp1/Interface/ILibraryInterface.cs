namespace ConsoleApp1.Interface
{
    public interface ILibraryInterface
    {
        public Book? GetMostBorrowedBook(List<Book> books);
    }
}
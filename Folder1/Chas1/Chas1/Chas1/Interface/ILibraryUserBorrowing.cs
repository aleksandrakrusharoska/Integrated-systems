namespace Chas1.Interface
{
    public interface ILibraryUserBorrowing
    {
        public void BorrowBook(Book book);
        public void ReturnBook(Book book);
    }
}
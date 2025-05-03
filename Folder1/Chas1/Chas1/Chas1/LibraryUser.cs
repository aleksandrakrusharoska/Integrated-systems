using Chas1.Interface;

namespace Chas1
{
    public abstract class LibraryUser : ILibraryUserBorrowing
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Book> BorrowedBooks { get; set; } = new();

        public abstract int BorrowLimit();

        public void BorrowBook(Book book)
        {
            if(BorrowedBooks.Count >= BorrowLimit())
            {
                Console.WriteLine("You have reached the borrow limit");
                return;
            }

            if (!book.IsAvailable())
            {
                Console.WriteLine("Book is not available");
                return;
            }

            Console.WriteLine($"{Name} borrowed the book: {book.Title}");
            BorrowedBooks.Add(book);
            book.BorrowedCopies++;
        }

        public void ReturnBook(Book book)
        {
            var borrowedBook = BorrowedBooks.FirstOrDefault(b => b.ISBN == book.ISBN);

            if (borrowedBook == null)
            {
                Console.WriteLine("Book is not borrowed");
                return;
            }

            Console.WriteLine($"{Name} returned the book: {book.Title}");
            BorrowedBooks.Remove(book);
            book.BorrowedCopies--;
        }
    }
}
using Chas1.Implementation;

namespace Chas1
{
    class Program
    {
        static void Main(string[] args)
        {
            var book1 = new Book()
            {
                Title = "ZokiPoki",
                Author = "Olivera Nikolovska",
                ISBN = "123456789",
                TotalCopies = 3
            };

            var book2 = new Book()
            {
                Title = "Kasni Porasni",
                Author = "Olivera Nikolovska",
                ISBN = "123456789",
                TotalCopies = 2 
            };


            var user1 = new RegularUser
            {
                Id = 1,
                Name = "Petar",
            };

            var user2 = new StudentUser
            {
                Id = 2,
                Name = "Igor",
            };

            var user3 = new ResearchUser
            {
                Id = 3,
                Name = "Filip",
            };

            user1.BorrowBook(book1);
            user2.BorrowBook(book1);

            user1.BorrowBook(book2);
            user2.BorrowBook(book2);
            user3.BorrowBook(book2);

            user1.ReturnBook(book1);
            user3.ReturnBook(book1);


            var mostBorrowedBook = new Library().GetMostBorrowedBook(new List<Book>
            {
                book1, book2
            });

            if (mostBorrowedBook == null)
            {
                Console.WriteLine("No books borrowed");
                return;
            }

            Console.WriteLine($"Most borrowed book: {mostBorrowedBook.Title}");
        }
    }
}
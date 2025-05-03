using Chas1.Interface;

namespace Chas1
{
    public class Book : IBookManagement
    {
        public string Title { get; set; }
        
        public string Author { get; set; }
        
        public string ISBN { get; set; }

        public int TotalCopies { get; set; }

        public int BorrowedCopies { get; set; }

        public bool IsAvailable()
        {
            return TotalCopies > BorrowedCopies;
        }
    }
}

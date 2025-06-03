using LibraryDomain.Identity;
using LibraryDomain.Relationship;

namespace LibraryDomain.Domain
{
    public class Order : BaseEntity
    {
        public string? OwnerId { get; set; }

        public virtual LibraryUser LibraryUser { get; set; }

        public virtual ICollection<BooksInOrder>? BooksInOrder { get; set; }
    }
}
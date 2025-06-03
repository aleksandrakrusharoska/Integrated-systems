using LibraryDomain.Domain;

namespace LibraryDomain.Relationship
{
    public class BooksInOrder : BaseEntity
    {
        public Guid BookId { get; set; }
        public virtual Book? Book { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }

        public int Quantity { get; set; }
    }
}

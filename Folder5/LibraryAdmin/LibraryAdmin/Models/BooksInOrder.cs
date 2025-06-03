namespace LibraryAdmin.Models
{
    public class BooksInOrder
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }
        public virtual Book? Book { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }

        public int Quantity { get; set; }
    }
}
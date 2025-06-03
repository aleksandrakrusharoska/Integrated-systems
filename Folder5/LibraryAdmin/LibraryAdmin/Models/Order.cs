namespace LibraryAdmin.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public string? OwnerId { get; set; }

        public virtual LibraryUser LibraryUser { get; set; }

        public virtual ICollection<BooksInOrder> BooksInOrder { get; set; } = new List<BooksInOrder>();
    }
}

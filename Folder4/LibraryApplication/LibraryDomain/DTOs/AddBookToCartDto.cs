namespace LibraryDomain.DTOs
{
    public class AddBookToCartDto
    {
        public Guid BookId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
namespace LibraryAdmin.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public required string Author { get; set; } = string.Empty;

        public string? Image { get; set; }

        public double Price { get; set; } = 0;
    }
}
using LibraryDomain.Relationship;

namespace LibraryDomain.Domain
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public required string Author { get; set; } = string.Empty;

        public string? Image { get; set; }

        public string Test { get; set; }


        public virtual ICollection<BooksInShoppingCart>? BooksInShoppingCart { get; set; }
    }
}
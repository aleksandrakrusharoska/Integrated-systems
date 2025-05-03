using LibraryDomain.Identity;
using LibraryDomain.Relationship;

namespace LibraryDomain.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }

        public virtual LibraryUser LibraryUser { get; set; }

        public virtual ICollection<BooksInShoppingCart>? BooksInShoppingCart { get; set; }
    }
}
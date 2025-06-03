using LibraryDomain.Domain;

namespace LibraryDomain.Relationship
{
    public class BooksInShoppingCart : BaseEntity
    {
        public Guid BookId { get; set; }
        public virtual Book? Book { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }

        public int Quantity { get; set; }
    }
}
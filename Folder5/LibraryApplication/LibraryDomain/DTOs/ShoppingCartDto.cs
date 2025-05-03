using LibraryDomain.Domain;
using LibraryDomain.Relationship;

namespace LibraryDomain.DTOs
{
    public class ShoppingCartDto
    {
        public List<BooksInShoppingCart> BooksInShoppingCart { get; set; } = new();

        public double TotalPrice { get; set; } = 0;
    }
}
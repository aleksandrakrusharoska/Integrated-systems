using LibraryDomain.Domain;

namespace LibraryService.Interfaces
{
    public interface IShoppingCartService
    {
        ShoppingCart? GetByOwner(string ownerId);
        
        ShoppingCart? GetByOwnerIncludeBooks(string ownerId);

        void OrderShoppingCart(string ownerId);
    }
}
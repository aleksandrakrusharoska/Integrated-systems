using LibraryDomain.Domain;

namespace LibraryService.Interfaces
{
    public interface IShoppingCartService
    {
        ShoppingCart? GetByOwner(string ownerId);
    }
}

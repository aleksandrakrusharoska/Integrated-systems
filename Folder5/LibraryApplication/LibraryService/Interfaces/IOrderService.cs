using LibraryDomain.Domain;

namespace LibraryService.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAll();

        Order? GetById(Guid id);
    }
}
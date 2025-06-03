using LibraryDomain.Domain;
using LibraryRepository.Interface;
using LibraryService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAll()
        {
            return _orderRepository
                .GetAll(selector: x => x,
                    include: x =>
                        x.Include(y => y.BooksInOrder)
                            .ThenInclude(u => u.Book)
                            .Include(o => o.LibraryUser))
                .ToList();
        }

        public Order? GetById(Guid id)
        {
            return _orderRepository
                .Get(selector: x => x,
                    filter: x => x.Id.Equals(id),
                    include: x =>
                        x.Include(y => y.BooksInOrder)
                            .ThenInclude(u => u.Book)
                            .Include(o => o.LibraryUser));
        }
    }
}
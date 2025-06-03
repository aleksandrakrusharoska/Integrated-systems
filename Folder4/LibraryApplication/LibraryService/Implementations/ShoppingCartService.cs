using LibraryDomain.Domain;
using LibraryDomain.Email;
using LibraryDomain.Relationship;
using LibraryRepository.Interface;
using LibraryService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BooksInOrder> _booksInOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService emailService;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository, IRepository<BooksInOrder> booksInOrderRepository, IUserRepository userRepository, IEmailService emailService)
        {
            this._shoppingCartRepository = shoppingCartRepository;
            this._orderRepository = orderRepository;
            _booksInOrderRepository = booksInOrderRepository;
            _userRepository = userRepository;
            this.emailService = emailService;
        }

        public ShoppingCart? GetByOwner(string ownerId)
        {
            return _shoppingCartRepository
                .Get(selector: cart => cart,
                    filter: cart =>
                        cart.OwnerId != null
                        && cart.OwnerId.Equals(ownerId));
        }

        public ShoppingCart? GetByOwnerIncludeBooks(string ownerId)
        {
            return _shoppingCartRepository
                .Get(selector: cart => cart,
                    filter: cart =>
                        cart.OwnerId != null
                        && cart.OwnerId.Equals(ownerId),
                    include: cart => 
                        cart.Include(i => i.BooksInShoppingCart)
                            .ThenInclude(y => y.Book));
        }

        public void OrderShoppingCart(string ownerId)
        {
            var shoppingCart = GetByOwnerIncludeBooks(ownerId);

            var order = new Order()
            {
                Id = Guid.NewGuid(),
                OwnerId = ownerId,
            };

            _orderRepository.Create(order);

            var booksInOrder = shoppingCart.BooksInShoppingCart.Select(b => new BooksInOrder
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Order = order,
                BookId = b.BookId,
                Book = b.Book,
                Quantity = b.Quantity
            }).ToList();

            foreach (var bookInOrder in booksInOrder)
            {
                _booksInOrderRepository.Create(bookInOrder);
            }

            shoppingCart.BooksInShoppingCart.Clear();
            _shoppingCartRepository.Update(shoppingCart);

            var user = _userRepository.GetUserById(ownerId);

            if (user is { Email: not null })
            {
                emailService.SendEmail(new EmailMessage
                {
                    To = user.Email,
                    Subject = "Order Confirmation",
                    Body = $"Your order with ID {order.Id} has been placed successfully. Thank you for shopping with us!"
                });
            }
        }
    }
}
using LibraryDomain.Domain;
using LibraryDomain.DTOs;
using LibraryDomain.Identity;
using LibraryService.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<LibraryUser> _userManager;

        public AdminController(IOrderService orderService, UserManager<LibraryUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAll();
            return Ok(orders);
        }


        [HttpGet("[action]/{id}")]
        public IActionResult GetOrderById(Guid? id)
        {
            if (id == null || Guid.NewGuid() == id.Value)
            {
                return BadRequest("Invalid Order ID.");
            }

            var order = _orderService.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ImportUsers([FromBody] List<CreateUserDto> users)
        {
            if (ModelState.IsValid)
            {
                foreach (var userDto in users)
                {
                    var existingUser = await _userManager.FindByEmailAsync(userDto.Email);

                    if (existingUser == null)
                    {
                        var user = new LibraryUser
                        {
                            FirstName = "First Name",
                            LastName = "Last Name",
                            UserName = userDto.Email,
                            NormalizedUserName = userDto.Email.ToUpper(),
                            Email = userDto.Email,
                            NormalizedEmail = userDto.Email.ToUpper(),
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = true,
                            UserShoppingCart = new ShoppingCart()
                        };

                        var result = await _userManager.CreateAsync(user, userDto.Password);
                        
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return BadRequest(ModelState);
                        }
                    }

                    return Ok(true);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
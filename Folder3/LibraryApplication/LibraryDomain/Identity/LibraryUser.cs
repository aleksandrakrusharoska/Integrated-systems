using LibraryDomain.Domain;
using Microsoft.AspNetCore.Identity;

namespace LibraryDomain.Identity
{
    public class LibraryUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public virtual ShoppingCart UserShoppingCart { get; set; }
    }
}
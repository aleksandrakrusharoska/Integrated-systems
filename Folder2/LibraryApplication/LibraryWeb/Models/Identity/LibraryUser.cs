using Microsoft.AspNetCore.Identity;

namespace LibraryWeb.Models.Identity
{
    public class LibraryUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace LibraryDomain.DTOs
{
    public class CreateUserDto
    {
        [Required] public required string Email { get; set; }

        [Required] public required string Password { get; set; }

        [Required] public required string ConfirmPassword { get; set; }
    }
}
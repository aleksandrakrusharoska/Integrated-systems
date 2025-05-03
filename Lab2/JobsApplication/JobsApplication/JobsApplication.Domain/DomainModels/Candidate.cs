using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsApplication.Domain.DomainModels
{
    public class Candidate
    {
        public Guid Id { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Application>? Applications { get; set; }
    }
}

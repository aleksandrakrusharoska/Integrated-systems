using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Domain.DomainModels
{
    // Visitor (FirstName, LastName, Email, MembershipDate, MembershipType)
    public class Visitor
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly MembershipDate { get; set; }
        public string MembershipType { get; set; }
        public virtual ICollection<Visit>? Visits { get; set; }
    }
}

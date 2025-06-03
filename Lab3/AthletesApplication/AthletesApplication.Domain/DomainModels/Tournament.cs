using AthletesApplication.Domain.IdentityModels;

namespace AthletesApplication.Domain.DomainModels
{
    public class Tournament : BaseEntity
    {
        public DateTime DateCreated{ get; set; }
        public AthletesApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsApplication.Domain.DomainModels
{
    public class Application : BaseEntity
    {
        public Candidate Candidate { get; set; }
        public Guid CandidateId { get; set; }
        public JobPosition JobPosition { get; set; }
        public Guid JobPositionId { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}

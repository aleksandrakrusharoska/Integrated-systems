using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsApplication.Domain.Dtos
{
    public class CreateApplicationDto
    {
        public Guid CandidateId { get; set; }
        public Guid JobPositionId { get; set; }
    }
}

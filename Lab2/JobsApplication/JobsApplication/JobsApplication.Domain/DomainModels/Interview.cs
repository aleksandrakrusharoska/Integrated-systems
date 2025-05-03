using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsApplication.Domain.DomainModels
{
    public class Interview
    {
        public Guid Id { get; set; }
        public DateOnly InterviewDate { get; set; }
        public string InterviewType { get; set; }
        public string Notes { get; set; }
        public JobPosition? JobPosition { get; set; }
        public Guid JobPositionId { get; set; }
    }
}

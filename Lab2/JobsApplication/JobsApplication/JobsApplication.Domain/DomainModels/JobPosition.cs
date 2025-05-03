using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsApplication.Domain.DomainModels
{
    public class JobPosition
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public ICollection<JobPosition>? JobPositions { get; set; }
        public ICollection<Application>? Applications { get; set; }
    }
}

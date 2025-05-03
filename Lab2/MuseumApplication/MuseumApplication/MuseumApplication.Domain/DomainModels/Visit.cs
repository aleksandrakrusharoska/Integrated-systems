using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Domain.DomainModels
{
    public class Visit
    {
        [Key]
        public Guid Id { get; set; }
        public Visitor Visitor { get; set; }
        public Guid VisitorId { get; set; }
        public Artifact Artifact { get; set; }
        public Guid ArtifactId { get; set; }
        public DateTime DateVisited { get; set; }
    }
}

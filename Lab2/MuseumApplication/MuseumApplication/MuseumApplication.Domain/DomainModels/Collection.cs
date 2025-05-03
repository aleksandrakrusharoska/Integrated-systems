using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Domain.DomainModels
{
    public class Collection
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Currator {  get; set; }
        public virtual ICollection<Artifact>? Artifacts { get; set; }
    }
}

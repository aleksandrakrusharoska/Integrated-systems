using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumApplication.Domain.DomainModels
{
    public class Artifact
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Origin { get; set; }
        public int Year { get; set; }
        [Required]
        public required string Description { get; set; }
        public Collection? Collection { get; set; }
        public Guid CollectionId { get; set; }
        public virtual ICollection<Visit>? Visits { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace LibraryWeb.Models.Domain
{
    public class Book : BaseEntity
    {
        [Display(Name = "Naslov")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(13)]
        public required string Author { get; set; } = string.Empty;
    }
}
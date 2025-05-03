namespace LibraryWeb.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
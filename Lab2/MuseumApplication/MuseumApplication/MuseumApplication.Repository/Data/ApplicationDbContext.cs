using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using MuseumApplication.Domain.DomainModels;
using MuseumApplication.Domain.IdentityModels;

namespace MuseumApplication.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artifact> Artifacts { get; set; }
        public virtual DbSet<Visitor> Visitors { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<MuseumApplicationUser> Users { get; set; }
    }
}

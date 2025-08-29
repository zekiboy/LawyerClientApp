using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Case> Cases { get; set; }

 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ClientCode unique olsun
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.ClientCode)
                .IsUnique();

            base.OnModelCreating(modelBuilder);

        }
    }
}
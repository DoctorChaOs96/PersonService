using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PesonService.DAL.Entity;

namespace PesonService.DAL
{
    public class DefaultDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public DefaultDbContext()
        {
        }
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }

        public DbSet<PersonEntity> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=PersonService;Trusted_Connection=True;TrustServerCertificate=True");
            }

#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif

            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PersonEntity>().Property(p => p.Avatar).HasColumnType("image");

            base.OnModelCreating(builder);
        }
    }
}

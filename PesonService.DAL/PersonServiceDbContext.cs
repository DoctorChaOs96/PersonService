using Microsoft.EntityFrameworkCore;
using PesonService.DAL.Entity;

namespace PesonService.DAL
{
    public class PersonServiceDbContext : DbContext
    {
        public PersonServiceDbContext(DbContextOptions<PersonServiceDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.Migrate();
        }

        public DbSet<PersonEntity> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonEntity>().Property(p => p.Avatar).HasColumnType("image");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }
    }
}

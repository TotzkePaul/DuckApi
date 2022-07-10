using Microsoft.EntityFrameworkCore;

namespace DuckApi.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("DuckApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Duck>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<Duck>()
                .HasIndex(p => new { p.Genus, p.Species })
                .IsUnique(); 
        }

        public DbSet<Duck> Ducks { get; set; }
    }
}

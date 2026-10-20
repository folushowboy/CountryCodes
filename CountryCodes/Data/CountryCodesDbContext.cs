using CountryCodes.Models;
using Microsoft.EntityFrameworkCore;

namespace CountryCodes.Data
{
    public class CountryCodesDbContext : DbContext
    {
        public CountryCodesDbContext(DbContextOptions<CountryCodesDbContext> options) : base(options)
        {
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryDetail> CountryDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>()
           .HasMany(c => c.CountryDetails)
           .WithOne(d => d.Country)
           .HasForeignKey(d => d.CountryId);
        }
    }

     
}

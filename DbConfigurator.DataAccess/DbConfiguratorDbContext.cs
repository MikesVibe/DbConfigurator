using DbConfigurator.Model.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Linq;

namespace DbConfigurator.DataAccess
{
    public partial class DbConfiguratorDbContext : DbContext
    {
        public DbConfiguratorDbContext()
        {

        }
        public DbSet<DistributionInformation> DistributionInformation { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<BuisnessUnit> BuisnessUnit { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<RecipientGroupCc> RecipientGroupCc { get; set; }
        public DbSet<RecipientGroupTo> RecipientGroupTo { get; set; }
        public DbSet<Recipient> Recipient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Setting up joining table for Recipients Groups
            modelBuilder.Entity<DistributionInformation>()
                .HasMany(g => g.RecipientsTo)
                .WithMany(r => r.RecipientGroupTo)
                .UsingEntity<RecipientGroupTo>();

            modelBuilder.Entity<DistributionInformation>()
                .HasMany(g => g.RecipientsCc)
                .WithMany(r => r.RecipientGroupCc)
                .UsingEntity<RecipientGroupCc>();


            var bUData = new GenericDataForTabels();
            foreach (var area in bUData.Areas.ToList())
            {
                modelBuilder.Entity<Area>().HasData(
                    new Area
                    {
                        Id = area.Id,
                        Name = area.Name
                    });
            }

            foreach (var buisnessUnit in bUData.BuisnessUnits.ToList())
            {
                modelBuilder.Entity<BuisnessUnit>().HasData(
                    new BuisnessUnit
                    {
                        Id = buisnessUnit.Id,
                        Name = buisnessUnit.Name
                    });
            }

            foreach (var country in bUData.Countries.ToList())
            {
                modelBuilder.Entity<Country>().HasData(
                    new Country
                    {
                        Id = country.Id,
                        CountryName = country.CountryName,
                        CountryCode = country.CountryCode
                    });
            }

            foreach (var priority in bUData.Priorities.ToList())
            {
                modelBuilder.Entity<Priority>().HasData(
                    new Priority
                    {
                        Id = priority.Id,
                        Name = priority.Name
                    });
            }



            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DbConfiguration"].ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}

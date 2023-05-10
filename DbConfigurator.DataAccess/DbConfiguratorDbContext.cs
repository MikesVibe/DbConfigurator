using DbConfigurator.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Linq;
using static DbConfigurator.DataAccess.DbConfiguratorDbContext;

namespace DbConfigurator.DataAccess
{
    public partial class DbConfiguratorDbContext : DbContext
    {
        public DbConfiguratorDbContext()
        {

        }
        public DbSet<DistributionInformation> DistributionInformation { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<AreaBuisnessUnit> AreaBuisnessUnit { get; set; }
        public DbSet<BuisnessUnit> BuisnessUnit { get; set; }
        public DbSet<BuisnessUnitCountry> BuisnessUnitCountry { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<RecipientGroup> RecipientGroup { get; set; }
        public DbSet<Recipient> Recipient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Setting up joining table for Area and BuisnessUnit
            modelBuilder.Entity<Area>()
                .HasMany(e => e.BuisnessUnits)
                .WithMany(e => e.Areas)
                .UsingEntity<AreaBuisnessUnit>();

            //Setting up joining table for BuisnessUnit and Country
            modelBuilder.Entity<BuisnessUnit>()
                .HasMany(e => e.Countries)
                .WithMany(e => e.BuisnessUnits)
                .UsingEntity<BuisnessUnitCountry>();

            //Setting up joining table for Recipients Groups
            modelBuilder.Entity<DistributionInformation>()
                .HasOne(d => d.RecipientGroup)
                .WithOne(r => r.DistributionInformation)
                .HasForeignKey<DistributionInformation>(d => d.RecipientGroupId)
                .IsRequired(false);

            modelBuilder.Entity<RecipientGroup>()
                .HasMany(g => g.RecipientsTo)
                .WithMany(r => r.RecipientGroupTo)
                .UsingEntity(j => j.ToTable("RecipientsGroupTo"));

            modelBuilder.Entity<RecipientGroup>()
                .HasMany(g => g.RecipientsCc)
                .WithMany(r => r.RecipientGroupCc)
                .UsingEntity(j => j.ToTable("RecipientsGroupCc"));


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

            foreach (var areaBuisnessUnit in bUData.AreaBuisnessUnits.ToList())
            {
                modelBuilder.Entity<AreaBuisnessUnit>().HasData(
                    new AreaBuisnessUnit
                    {
                        Id = areaBuisnessUnit.Id,
                        AreaId = areaBuisnessUnit.AreaId,
                        BuisnessUnitId = areaBuisnessUnit.BuisnessUnitId
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

            foreach (var buisnessUnitCountry in bUData.BuisnessUnitCountries.ToList())
            {
                modelBuilder.Entity<BuisnessUnitCountry>().HasData(
                    new BuisnessUnitCountry
                    {
                        Id = buisnessUnitCountry.Id,
                        BuisnessUnitId = buisnessUnitCountry.BuisnessUnitId,
                        CountryId = buisnessUnitCountry.CountryId
                    });
            }

            foreach (var country in bUData.Countries.ToList())
            {
                modelBuilder.Entity<Country>().HasData(
                    new Country
                    {
                        Id = country.Id,
                        Name = country.Name,
                        ShortCode = country.ShortCode
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

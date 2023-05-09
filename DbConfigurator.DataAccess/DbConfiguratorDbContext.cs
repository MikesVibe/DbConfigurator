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
        public DbSet<BuisnessUnit> BuisnessUnit { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<RecipientsGroup> RecipientsGroup { get; set; }
        public DbSet<Recipient> Recipient { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DistributionInformation>()
                .HasOne(d => d.RecipientsGroup)
                .WithOne(r => r.DistributionInformation)
                .HasForeignKey<DistributionInformation>(d => d.RecipientsGroupId)
                .IsRequired(false);

            modelBuilder.Entity<RecipientsGroup>()
                .HasMany(g => g.RecipientsTo)
                .WithMany(r => r.RecipientsGroupsTo)
                .UsingEntity(j => j.ToTable("RecipientsGroupTo"));

            modelBuilder.Entity<RecipientsGroup>()
                .HasMany(g => g.RecipientsCc)
                .WithMany(r => r.RecipientsGroupsCc)
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

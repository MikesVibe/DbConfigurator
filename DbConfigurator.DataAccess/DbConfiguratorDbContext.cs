using DbConfigurator.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DbConfiguration"].ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();



            //optionsBuilder.UseSqlServer("server=\"MIKI-PC\\SQLEXPRESS01\";database=\"DbConfiguration\";trusted_connection=true;Integrated Security=True;Encrypt=False");
        }
    }
}

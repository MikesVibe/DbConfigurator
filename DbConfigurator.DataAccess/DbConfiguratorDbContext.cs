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



            List<Area> areasTable = new List<Area>();
            List<BuisnessUnit> buisnessUnitsTable = new List<BuisnessUnit>();

            var temp = new BUData();
            var gbuData = temp.GetBUData();

            //Seed Area table in DataBase
            var gbuArea = gbuData.Area.Distinct().ToList();
            for (int i = 1; i <= gbuArea.Count; i++)
            {
                modelBuilder.Entity<Area>().HasData(new Area { Id = i, Name = gbuArea[i - 1] });

                var area = new Area();
                area.Id = i;
                area.Name = gbuArea[i - 1];
                areasTable.Add(area);
            }


            //Seed BuisnessUnits table in DataBase
            List<string> gbuBuisnessUnit = gbuData.CountryCluster.Distinct().ToList();
            List<int> AreaIds = new List<int>();
            foreach (var countryCluster in gbuBuisnessUnit)
            {
                int index = gbuData.CountryCluster.IndexOf(countryCluster);

                var areaId = areasTable.Where(a => a.Name == gbuData.Area.ElementAt(index)).ToList().First().Id;
                AreaIds.Add(areaId);
            }

            for (int i = 1; i <= gbuBuisnessUnit.Count; i++)
            {
                modelBuilder.Entity<BuisnessUnit>().HasData(
    new BuisnessUnit
    {
        Id = i,
        Name = gbuBuisnessUnit[i - 1]
    });
                var businessUnit = new BuisnessUnit();
                businessUnit.Id = i;
                businessUnit.Name = gbuBuisnessUnit[i - 1];
                buisnessUnitsTable.Add(businessUnit);
            }

            //Seed Country table in DataBase
            var gbuCountry = gbuData.Country.ToList();

            var gbuCountryCode = gbuData.CountryCode.ToList();


            List<int> countryBusinessUnitIdList = new List<int>();
            for (int i = 0; i < gbuData.CountryCluster.Count; i++)
            {
                var buId = buisnessUnitsTable.Where(a => a.Name == gbuData.CountryCluster.ElementAt(i)).ToList().First().Id;

                countryBusinessUnitIdList.Add(buId);
            }
            for (int i = 1; i <= gbuCountryCode.Count; i++)
            {
                modelBuilder.Entity<Country>().HasData(
                    new Country
                    {
                        Id = i,
                        Name = gbuCountry[i - 1],
                        ShortCode = gbuCountryCode[i - 1]
                    });
            }

            modelBuilder.Entity<Recipient>().HasData(
                    new Recipient
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "John.Doe@company.net"
                    },
                     new Recipient
                     {
                         Id = 2,
                         FirstName = "Josh",
                         LastName = "Smith",
                         Email = "Josh.Smith@company.net"
                     }
                    );

            modelBuilder.Entity<Priority>().HasData(
                new Priority
                {
                    Id = 1,
                    Name = "P1"
                },
                new Priority
                {
                    Id = 2,
                    Name = "P2"
                },
                new Priority
                {
                    Id = 3,
                    Name = "P3"
                },
                new Priority
                {
                    Id = 4,
                    Name = "P4"
                },
                new Priority
                {
                    Id = 99,
                    Name = "Any"
                });



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

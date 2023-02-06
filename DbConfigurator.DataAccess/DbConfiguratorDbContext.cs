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
        public DbSet<DistributionInformation> DistributionInformations { get; set; }
        public DbSet<BuisnessUnit> BuisnessUnits { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<RecipientsGroup> RecipientsGroups { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<DestinationField> DestinationFields { get; set; }
        public DbSet<DistributionInformationView> DistributionInformationViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DistributionInformationView>()
            .ToView("DistributionInformationView");

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
        Name = gbuBuisnessUnit[i - 1],
        AreaId = AreaIds[i - 1],

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
                        ShortCode = gbuCountryCode[i - 1],
                        BuisnessUnitId = countryBusinessUnitIdList[i - 1]
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
                    Id = 5,
                    Name = "Any"
                });


            modelBuilder.Entity<DistributionInformation>().HasData(
                new DistributionInformation
                {
                    Id = 1,
                    CountryId = 4,
                    PriorityId = 5
                }
                
                );

            modelBuilder.Entity<RecipientsGroup>().HasData(
                new RecipientsGroup
                {
                    Id = 1,
                    DestinationFieldId = 1,
                    DistributionInformationId = 1,
                },
                new RecipientsGroup
                {
                    Id = 2,
                    DestinationFieldId = 2,
                    DistributionInformationId = 1,
                }
                );
            modelBuilder.Entity<DestinationField>().HasData(
                new DestinationField
                {
                    Id = 1,
                    Name = "TO",
                },
                new DestinationField
                {
                    Id = 2,
                    Name = "CC",
                }
                );
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DbConfiguration"].ConnectionString);
            //optionsBuilder.UseSqlServer("server=\"MIKI-PC\\SQLEXPRESS01\";database=\"DbConfiguration\";trusted_connection=true;Integrated Security=True;Encrypt=False");
        }
    }
}

using DbConfigurator.Model.DTOs.Creation;
using DbConfigurator.Model.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess
{
    public class Seeder : ISeeder
    {
        private readonly DbConfiguratorDbContext _dbConfiguratorDbContext;

        public Seeder(DbConfiguratorDbContext dbConfiguratorDbContext)
        {
            _dbConfiguratorDbContext = dbConfiguratorDbContext;
        }



        public async Task SeedRecipients()
        {
            await _dbConfiguratorDbContext.Set<Recipient>().AddRangeAsync(
                               new Recipient
                               {
                                   FirstName = "John",
                                   LastName = "Doe",
                                   Email = "John.Doe@company.net"
                               }, new Recipient
                               {
                                   FirstName = "Josh",
                                   LastName = "Smith",
                                   Email = "Josh.Smith@company.net"
                               }
                    );


            await _dbConfiguratorDbContext.SaveChangesAsync();
        }

        public async Task SeedRegions(string regionsAsJson)
        {
            var regionsForCreation = JsonConvert.DeserializeObject<IEnumerable<RegionForCreationDto>>(regionsAsJson);

            var areas = await _dbConfiguratorDbContext.Set<Area>().ToListAsync();
            var buisnessUnits = await _dbConfiguratorDbContext.Set<BuisnessUnit>().ToListAsync();
            var countries = await _dbConfiguratorDbContext.Set<Country>().ToListAsync();

            var regions = new List<Region>();

            foreach (var region in regionsForCreation)
            {
                var area = areas.Where(a => a.Name == region.Area).First();
                var buisnessUnit = buisnessUnits.Where(bu => bu.Name == region.BuisnessUnit).First();
                var country = countries.Where(c => c.CountryName == region.Country).First();

                regions.Add(new Region { Area = area, BuisnessUnit = buisnessUnit, Country = country });
            }



            await _dbConfiguratorDbContext.Set<Region>().AddRangeAsync(regions);


            await _dbConfiguratorDbContext.SaveChangesAsync();
        }
        public async Task SeedDistributionInformation()
        {
            await _dbConfiguratorDbContext.Set<DistributionInformation>().AddRangeAsync(
                new DistributionInformation
                {
                    RegionId = 1,
                    PriorityId = 1,
                },
                new DistributionInformation
                {
                    RegionId = 13,
                    PriorityId = 2,
                },
                new DistributionInformation
                {
                    RegionId = 4,
                    PriorityId = 2,
                },
                new DistributionInformation
                {
                    RegionId = 23,
                    PriorityId = 3,
                },
                new DistributionInformation
                {
                    RegionId = 65,
                    PriorityId = 99,
                },
                new DistributionInformation
                {
                    RegionId = 1,
                    PriorityId = 4,
                }
                );


            await _dbConfiguratorDbContext.SaveChangesAsync();
        }

        public async Task<bool> AnyRegionInDatabaseAsync()
        {
            return await _dbConfiguratorDbContext.Set<Region>().AnyAsync();
        }
        public async Task<bool> AnyRecipientInDatabaseAsync()
        {
            return await _dbConfiguratorDbContext.Set<Recipient>().AnyAsync();
        }
        public async Task<bool> AnyDistributionInformationAsync()
        {
            return await _dbConfiguratorDbContext.Set<DistributionInformation>().AnyAsync();
        }
    }
}

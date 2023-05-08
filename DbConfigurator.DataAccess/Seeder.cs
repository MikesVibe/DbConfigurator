using DbConfigurator.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DbConfigurator.DataAccess.DbConfiguratorDbContext;

namespace DbConfigurator.DataAccess
{
    public class Seeder : ISeeder
    {
        private readonly DbConfiguratorDbContext _dbConfiguratorDbContext;

        public Seeder(DbConfiguratorDbContext dbConfiguratorDbContext)
        {
            _dbConfiguratorDbContext = dbConfiguratorDbContext;
        }

        public async Task Seed()
        {
            if (_dbConfiguratorDbContext.Set<Area>().Any())
                return;

            List<Area> areasTable = new List<Area>();
            List<BuisnessUnit> buisnessUnitsTable = new List<BuisnessUnit>();

            var temp = new BUData();
            var gbuData = temp.GetBUData();


            //Seed Area table in DataBase
            var gbuArea = gbuData.Area.Distinct().ToList();
            for (int i = 1; i <= gbuArea.Count; i++)
            {
                await _dbConfiguratorDbContext.Set<Area>().AddAsync(new Area { Name = gbuArea[i - 1] });
            }

            //Seed BuisnessUnits table in DataBase
            List<string> gbuBuisnessUnit = gbuData.CountryCluster.Distinct().ToList();

            for (int i = 1; i <= gbuBuisnessUnit.Count; i++)
            {
                await _dbConfiguratorDbContext.Set<BuisnessUnit>().AddAsync(
                        new BuisnessUnit
                        {
                            Name = gbuBuisnessUnit[i - 1]
                        });
            }

            ////Seed Country table in DataBase
            var gbuCountry = gbuData.Country.ToList();
            var gbuCountryCode = gbuData.CountryCode.ToList();


            for (int i = 1; i <= gbuCountryCode.Count; i++)
            {
                await _dbConfiguratorDbContext.Set<Country>().AddAsync(
                       new Country
                       {
                           Name = gbuCountry[i - 1],
                           ShortCode = gbuCountryCode[i - 1]
                       });
            }
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

            List<string> priorityNames = new List<string>() { "P1", "P2", "P3", "P4", "Any" };
            foreach (string priorityName in priorityNames)
            {
                await _dbConfiguratorDbContext.Set<Priority>().AddRangeAsync(
                    new Priority
                    {
                        Name = priorityName
                    });
            }


            await _dbConfiguratorDbContext.SaveChangesAsync();
        }
    }
}

using DbConfigurator.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

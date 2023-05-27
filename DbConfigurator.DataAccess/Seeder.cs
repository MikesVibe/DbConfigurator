using DbConfigurator.Model.Entities;
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
            if (await _dbConfiguratorDbContext.Set<Recipient>().AnyAsync())
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


            await _dbConfiguratorDbContext.SaveChangesAsync();
        }
    }
}

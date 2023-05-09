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


            //await _dbConfiguratorDbContext.Set<Recipient>().AddRangeAsync(
            //                   new Recipient
            //                   {
            //                       FirstName = "John",
            //                       LastName = "Doe",
            //                       Email = "John.Doe@company.net"
            //                   }, new Recipient
            //                   {
            //                       FirstName = "Josh",
            //                       LastName = "Smith",
            //                       Email = "Josh.Smith@company.net"
            //                   }
            //        );

            //int[] AreaIds = { 1, 1, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 1, 2, 3, 4, 5, 99 };
            //int[] BuisnessUnitIds = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 99, 99, 99, 99, 99, 99 };

            //for (int i = 0; i < AreaIds.Count(); i++)
            //{
            //    var businessUnit = _dbConfiguratorDbContext.AreaBuisnessUnit.Add(
            //        new AreaBuisnessUnit 
            //        {
            //            AreaId = AreaIds[i],
            //            BuisnessUnitId = BuisnessUnitIds[i]
            //        });


            //}


            //await _dbConfiguratorDbContext.SaveChangesAsync();
        }
    }
}

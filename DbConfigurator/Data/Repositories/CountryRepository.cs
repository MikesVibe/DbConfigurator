using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Data.Repositories
{
    public class CountryRepository : GenericRepository<Country, DbConfiguratorDbContext>, ICountryRepository
    {
        public CountryRepository(DbConfiguratorDbContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Country>> GetAllAsync()
        {
            var collection = await Context.Set<Country>().Include(c => c.BuisnessUnit.Area).ToListAsync();

            return collection;
        }

    }
}

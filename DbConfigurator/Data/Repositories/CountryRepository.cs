using DbConfigurator.DataAccess;
using DbConfigurator.Model;
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
    }
}

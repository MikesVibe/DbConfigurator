using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Data.Repositories
{
    public class BuisnessRepository : GenericRepository<BuisnessUnit, DbConfiguratorDbContext>, IBuisnessRepository
    {
        public BuisnessRepository(DbConfiguratorDbContext context) : base(context)
        {
        }
        
    }
}

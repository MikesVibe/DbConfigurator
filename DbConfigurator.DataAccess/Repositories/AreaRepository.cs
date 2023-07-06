using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Repositories
{
    public class AreaRepository : GenericRepository<Area>
    {
        public AreaRepository(DbConfiguratorDbContext dbContext) : base(dbContext)
        {
        }
    }
}

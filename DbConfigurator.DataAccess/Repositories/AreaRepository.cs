using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.Entities.Core;

namespace DbConfigurator.DataAccess.Repositories
{
    public class AreaRepository : GenericRepository<Area>
    {
        public AreaRepository(DbConfiguratorDbContext dbContext) : base(dbContext)
        {
        }
    }
}

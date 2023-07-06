using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.Entities.Core;

namespace DbConfigurator.DataAccess.Repositories
{
    public class PriorityRepository : GenericRepository<Priority>
    {
        public PriorityRepository(DbConfiguratorDbContext dbContext) : base(dbContext)
        {
        }
    }
}

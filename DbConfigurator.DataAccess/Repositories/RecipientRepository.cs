using DbConfigurator.DataAccess.Repository;
using DbConfigurator.Model.Entities.Core;

namespace DbConfigurator.DataAccess.Repositories
{
    public class RecipientRepository : GenericRepository<Recipient>
    {
        public RecipientRepository(DbConfiguratorDbContext dbContext) : base(dbContext)
        {
        }
    }
}

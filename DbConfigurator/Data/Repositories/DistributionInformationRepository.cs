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
    public class DistributionInformationRepository : GenericRepository<DistributionInformation, DbConfiguratorDbContext>,
        IDistributionInformationRepository
    {
        public DistributionInformationRepository(DbConfiguratorDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<DistributionInformation>> GetAllAsync()
        {
            var collection = await Context.Set<DistributionInformation>()
                .Include(c => c.BuisnessUnit.Area)
                .Include(g => g.BuisnessUnit.Countries.Where(c => c.BuisnessUnitId == c.BuisnessUnitId))
                .Include(p => p.Priority).ToListAsync();

            return collection;
        }
    }
}

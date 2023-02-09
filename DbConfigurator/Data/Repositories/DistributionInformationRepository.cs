using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
                .Include(c => c.Country.BuisnessUnit.Area)
                .Include(c => c.RecipientsGroup_Collection)
                .ThenInclude(rg => rg.DestinationField)
                .ThenInclude(r => r.RecipientsGroups)
                .ThenInclude(t => t.Recipients)
                .Include(p => p.Priority)
                .ToListAsync();


            return collection;
        }
        public async Task<IEnumerable<Priority>> GetAllPrioritiesAsync()
        {
            var collection = await Context.Set<Priority>().ToListAsync();
            return collection;
        }

        public async Task<DistributionInformation> GetByIdAsync(int id)
        {
            return await Context.Set<DistributionInformation>().Where(di => di.Id == id)
                .Include(c => c.Country.BuisnessUnit.Area)
                .Include(c => c.RecipientsGroup_Collection)
                .ThenInclude(rg => rg.DestinationField)
                .ThenInclude(r => r.RecipientsGroups)
                .ThenInclude(t => t.Recipients)
                .Include(p => p.Priority)
                .FirstAsync();
        }
        public async Task<Country> GetNewCountryById(int id)
        {
            return await Context.Set<Country>().Where(di => di.Id == id)
                .Include(c => c.BuisnessUnit.Area)
                .FirstAsync();
        }

        public async Task<Priority> GetNewPriorityById(int id)
        {
            return await Context.Set<Priority>().Where(di => di.Id == id)
                .FirstAsync();
        }
    }
}

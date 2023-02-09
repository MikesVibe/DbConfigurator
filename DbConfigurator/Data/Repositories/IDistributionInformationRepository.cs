using DbConfigurator.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Data.Repositories
{
    public interface IDistributionInformationRepository : IGenericRepository<DistributionInformation>
    {
        Task<IEnumerable<Priority>> GetAllPrioritiesAsync();
        Task<DistributionInformation> GetByIdAsync(int id);
        Task<Country> GetNewCountryById(int id);
        Task<Priority> GetNewPriorityById(int id);
        void ReloadEntryPriority(DistributionInformation dis);
    }
}
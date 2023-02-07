using DbConfigurator.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Data.Repositories
{
    public interface IDistributionInformationRepository : IGenericRepository<DistributionInformation>
    {
        Task<IEnumerable<Priority>> GetAllPrioritiesAsync();
    }
}
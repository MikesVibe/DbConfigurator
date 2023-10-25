using DbConfigurator.Model.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public interface IRegionService : IDataService<Region>
    {
        Task<IEnumerable<Area>> GetAllAreasAsync();
        Task<IEnumerable<BusinessUnit>> GetAllBusinessUnitsAsync();
        Task<IEnumerable<Country>> GetAllCountriesAsync();
    }
}
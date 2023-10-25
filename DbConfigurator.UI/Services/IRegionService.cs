using DbConfigurator.Model.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public interface IRegionService : IDataService<Region>
    {
        Task<ICollection<Area>> GetAllAreasAsync();
        Task<ICollection<BusinessUnit>> GetAllBusinessUnitsAsync();
        Task<ICollection<Country>> GetAllCountriesAsync();
    }
}
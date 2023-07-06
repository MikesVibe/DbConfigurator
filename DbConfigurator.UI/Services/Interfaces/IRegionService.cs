using DbConfigurator.Model.DTOs.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services.Interfaces
{
    public interface IRegionService : IGenericDataService<RegionDto>
    {
        Task<IEnumerable<AreaDto>> GetAreasAsync();
        Task<IEnumerable<BuisnessUnitDto>> GetBuisnessUnitsAsync();
        Task<IEnumerable<CountryDto>> GetCountriesAsync();
    }
}
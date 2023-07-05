using DbConfigurator.Model.DTOs.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services.Interfaces
{
    public interface IRegionService
    {
        Task<IEnumerable<AreaDto>> GetAreasAsync();
        Task<IEnumerable<BuisnessUnitDto>> GetBuisnessUnitsAsync();
        Task<List<BuisnessUnitDto>> GetBuisnessUnitsAsync(int? areaId = null);
        Task<IEnumerable<CountryDto>> GetCountriesAsync();
        Task<List<CountryDto>> GetCountriesAsync(int? buisnessUnitId = null);
        Task<RegionDto?> GetRegionByIdAsync(int id);
    }
}
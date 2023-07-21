using DbConfigurator.Model.DTOs.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services.Interfaces
{
    public interface IRegionService : IGenericDataService<RegionDto>
    {
        Task<ICollection<AreaDto>> GetAllAreasAsync();
        Task<ICollection<BuisnessUnitDto>> GetAllBuisnessUnitsAsync();
        Task<ICollection<CountryDto>> GetAllCountriesAsync();
    }
}
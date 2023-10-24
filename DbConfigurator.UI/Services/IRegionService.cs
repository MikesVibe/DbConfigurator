using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.UI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public interface IRegionService : IDataService<RegionDto>
    {
        Task<ICollection<AreaDto>> GetAllAreasAsync();
        Task<ICollection<BusinessUnitDto>> GetAllBusinessUnitsAsync();
        Task<ICollection<CountryDto>> GetAllCountriesAsync();
    }
}
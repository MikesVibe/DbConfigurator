using DbConfigurator.DataAccess.DTOs.AreaDto;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public interface IRegionService : IDataService<Area>
    {
        Task<ICollection<AreaDto>> GetAllAreasAsync();
        Task<ICollection<BusinessUnitDto>> GetAllBusinessUnitsAsync();
        Task<ICollection<CountryDto>> GetAllCountriesAsync();
    }
}
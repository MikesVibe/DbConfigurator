using DbConfigurator.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Data.Repositories
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<IEnumerable<Area>> GetAllAreasAsync();
        Task<IEnumerable<BuisnessUnit>> GetAllBuisnessUnitsAsync();
        Task<IEnumerable<Country>> GetAllCountriesAsync();
    }
}
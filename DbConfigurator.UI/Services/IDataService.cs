using DbConfigurator.Model.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public interface IDataService<TEntity>
        where TEntity : class, new()
    {
        Task<bool> ExistsAsync(int entityId);
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();

    }
}
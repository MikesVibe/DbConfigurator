using DbConfigurator.Model.Contracts;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public interface IDataService<TEntity>
        where TEntity : class, new()
    {
        TEntity Create(TEntity createDto);
        TEntity Update(TEntity createDto);
        Task<TEntity> DeleteAsync(int id);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();

    }
}
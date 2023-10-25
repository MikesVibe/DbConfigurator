using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Controllers
{
    public class BaseController<TCreateDto, TUpdateDto, TEntity>
        where TCreateDto : class
        where TUpdateDto : class
        where TEntity : class, new()
    {
        protected readonly HttpClient _httpClient;

        public BaseController()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:8443/api/")
            };
        }

        public TEntity Create(TCreateDto createDto)
        {
            return new TEntity();
        }
        public TEntity Update(TUpdateDto createDto)
        {
            return new TEntity();
        }
        public async Task<TEntity> Delete(int id)
        {
            await Task.CompletedTask;
            return new TEntity();
        }
        public async Task<TEntity> GetById(int id)
        {
            var dto = await _httpClient.GetFromJsonAsync<IEnumerable<TEntity>>($"area/{id}");
            return new TEntity();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var dto = await _httpClient.GetFromJsonAsync<TEntity>($"area/all");

            return new List<TEntity>();
        }
    }
}

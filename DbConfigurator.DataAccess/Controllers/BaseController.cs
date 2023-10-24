using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Controllers
{
    public class BaseController<TCreateDto, TUpdateDto, TDto> 
        where TCreateDto : class
        where TUpdateDto : class
        where TDto : class, new()
    {
        protected readonly HttpClient _httpClient;

        public BaseController()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:8443/api/")
            };
        }

        public TDto Create(TCreateDto createDto)
        {
            return new TDto();
        }
        public TDto Update(TUpdateDto createDto)
        {
            return new TDto();
        }
        public async Task<TDto> Delete(int id)
        {
            await Task.CompletedTask;
            return new TDto();
        }
        public async Task<TDto> GetById(int id)
        {
            var dto = await  _httpClient.GetFromJsonAsync<IEnumerable<TDto>>($"area/{id}");
            return new TDto();
        }
        public virtual async Task<IEnumerable<TDto>> GetAll()
        {
            var dto = await _httpClient.GetFromJsonAsync<TDto>($"area/all");
            
            return new List<TDto>();
        }
    }
}

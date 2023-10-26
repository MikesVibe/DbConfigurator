using DbConfigurator.DataAccess;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Startup;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Base
{
    public class GenericDataService<TCreateDto, TUpdateDto, TEntity> : IDataService<TEntity>
        where TEntity : class, new()
    {
        private readonly IDbConfiguratorApiClient _client;
        protected readonly AutoMapperConfig _mapper;
        protected readonly string _controllerName;
        private IEnumerable<TEntity> _entities = new List<TEntity>();
        private bool _entitiesLoaded;

        public GenericDataService(
            IDbConfiguratorApiClient client,
            AutoMapperConfig mapper, string controllerName)
        {
            _client = client;
            _mapper = mapper;
            _controllerName = controllerName;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            var toCreate = _mapper.Mapper.Map<TCreateDto>(entity);

            using (HttpClient client = _client.CreateClient())
            {
                // Convert ClassDto to JSON
                string jsonData = JsonSerializer.Serialize(toCreate);

                // Set content type to JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Create the HTTP request content
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send POST request
                HttpResponseMessage response = await client.PostAsync($"{_controllerName}", content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    //Console.WriteLine("Data sent successfully!");
                }
                else
                {
                    //Console.WriteLine($"Error sending data. Status code: {response.StatusCode}");
                }
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await Task.CompletedTask;
            return true;
        }

        public async Task<bool> ExistsAsync(int entityId)
        {
            await Task.CompletedTask;
            return true;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (_entitiesLoaded == false)
            {
                await LoadEntities();
                _entitiesLoaded = true;
            }

            return _entities;
        }



        //public async Task<TEntity> GetByIdAsync(int id)
        //{
        //    await Task.CompletedTask;
        //    return new TEntity();
        //}

        public async Task<bool> UpdateAsync(TEntity createDto)
        {
            await Task.CompletedTask;
            return true;
        }

        private async Task LoadEntities()
        {
            using (var client = _client.CreateClient())
            {
                try
                {
                    var dto = await client.GetFromJsonAsync<IEnumerable<TEntity>>($"{_controllerName}/all");
                    _entities = _mapper.Mapper.Map<IEnumerable<TEntity>>(dto);
                }
                catch
                {
                }
            }
        }
    }
}

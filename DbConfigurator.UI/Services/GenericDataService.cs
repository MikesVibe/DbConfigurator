using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.DTOs;
using DbConfigurator.Core.Contracts;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Startup;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DbConfigurator.DataAccess.Errors;

namespace DbConfigurator.UI.Base
{
    public class GenericDataService<TCreateDto, TUpdateDto, TEntity> : IDataService<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly IDbConfiguratorApiClient _client;
        private readonly IStatusService _statusService;
        protected readonly AutoMapperConfig _mapper;
        protected readonly string _controllerName;
        private List<TEntity> _entities = new List<TEntity>();
        private bool _entitiesLoaded;
        protected bool _hasChanges;

        public GenericDataService(
            IDbConfiguratorApiClient client,
            IStatusService statusService,
            AutoMapperConfig mapper, string controllerName)
        {
            _client = client;
            _statusService = statusService;
            _mapper = mapper;
            _controllerName = controllerName;
        }

        public bool IsConnected
        {
            get { return _statusService.IsConnected; }
        }
        public async Task<bool> CanConnect()
        {
            return await _statusService.CanConnect();
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

                // Send PUT request
                HttpResponseMessage response = await client.PostAsync($"{_controllerName}", content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (responseContent is not null)
                    {
                        // Deserialize the response content to TEntityDto
                        var serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var responseDto = JsonSerializer.Deserialize<EntityIdDto>(responseContent, serializerOptions)!;
                        
                        entity.Id = responseDto!.Id;
                    }


                    //Console.WriteLine("Data sent successfully!");
                    _entities.Add(entity);
                    _hasChanges = true;
                    return true;
                }
                else
                {
                    //Console.WriteLine($"Error sending data. Status code: {response.StatusCode}");
                    return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (HttpClient client = _client.CreateClient())
            {

                // Send DELETE requests
                HttpResponseMessage response = await client.DeleteAsync($"{_controllerName}?id={id}");

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    var entity = _entities.Where(e => e.Id == id).Single();
                    _entities.Remove(entity);
                    _hasChanges = true;
                    return true;
                }
                else
                {
                    //Entity could not be deleted
                    //Log some error mesage here
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }

                    return false;
                }
            }
        }

        public async Task<bool> ExistsAsync(int entityId)
        {
            await Task.CompletedTask;
            return true;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (_entitiesLoaded == false || ChildrenHaveChanges())
            {
                await LoadEntities();
                _entitiesLoaded = true;
            }
            _hasChanges = false;
            return _entities;
        }


        public virtual async Task<Result<IEnumerable<TEntity>>> GetAllAsyncResult()
        {
            if (_entitiesLoaded == false || ChildrenHaveChanges())
            {
                var result = await LoadEntities();
                if (result.IsFailed)
                {
                    return Result.Fail(result.Errors);
                }
                _entitiesLoaded = true;
            }
            _hasChanges = false;
            return _entities;
        }


        //public async Task<TEntity> GetByIdAsync(int id)
        //{
        //    await Task.CompletedTask;
        //    return new TEntity();
        //}

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var toUpdate = _mapper.Mapper.Map<TUpdateDto>(entity);

            using (HttpClient client = _client.CreateClient())
            {
                // Convert ClassDto to JSON
                string jsonData = JsonSerializer.Serialize(toUpdate);

                // Set content type to JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Create the HTTP request content
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send POST request
                HttpResponseMessage response = await client.PutAsync($"{_controllerName}", content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    //var entityInList = _entities.Where(e => e.Id == entity.Id).;
                    //_entities.ElementAt(entityInList).
                    //Console.WriteLine("Data sent successfully!");
                    _hasChanges = true;
                    return true;
                }
                else
                {
                    //Entity could not be updated
                    //Log some error mesage here
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }

                    return false;
                }
            }
        }

        private async Task<Result> LoadEntities()
        {
            using (var client = _client.CreateClient())
            {
                try
                {
                    var dto = await client.GetFromJsonAsync<IEnumerable<TEntity>>($"{_controllerName}/all");
                    _entities = _mapper.Mapper.Map<List<TEntity>>(dto);
                    return Result.Ok();
                }
                catch(HttpRequestException ex)
                {
                    return Result.Fail(new AuthorizationError());
                }
                catch
                {
                    return Result.Fail("");
                }
            }
        }


        public virtual bool HasChanges()
        {
            return _hasChanges;
        }

        public virtual bool ChildrenHaveChanges()
        {
            return false;
        }
    }
}

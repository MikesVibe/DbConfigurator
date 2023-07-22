﻿using DbConfigurator.Model.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services.Interfaces
{
    public interface IDataService<TDto> where TDto : IEntityDto
    {
        TDto Add(TDto dto);
        Task<int> AddAsync(TDto dto);
        Task<IEnumerable<TDto>> GetAllAsync();
        TDto GetById(int id);
        Task<TDto> GetByIdAsync(int id);
        bool RemoveById(int id);
        Task<bool> RemoveByIdAsync(int id);
        bool Update(TDto dto);
        Task<bool> UpdateAsync(TDto dto);
    }
}
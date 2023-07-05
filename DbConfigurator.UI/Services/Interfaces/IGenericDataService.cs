﻿using DbConfigurator.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services.Interfaces
{
    public interface IGenericDataService<TDto> where TDto : IEntityDto
    {
        Task<TDto> AddAsync(TDto dto);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> UpdateAsync(TDto dto);
    }
}
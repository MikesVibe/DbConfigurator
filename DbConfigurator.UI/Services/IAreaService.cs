﻿using DbConfigurator.DataAccess.DTOs.AreaDto;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;

namespace DbConfigurator.UI.Services
{
    public interface IAreaService : IDataService<CreateAreaDto, UpdateAreaDto, Area>
    {
    }
}
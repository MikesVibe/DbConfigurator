﻿using DbConfigurator.DataAccess.Repositories;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Features.Areas
{
    public class AreaService : GenericDataService<Area, AreaDto, AreaRepository>, IAreaService
    {
        public AreaService(AreaRepository repository, AutoMapperConfig autoMapper)
            : base(repository, autoMapper)
        {
        }
    }
}
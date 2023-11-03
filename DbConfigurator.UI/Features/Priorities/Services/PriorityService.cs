﻿using DbConfigurator.DataAccess.DTOs.CountryDtos;
using DbConfigurator.DataAccess;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Features.Countries.Services;
using DbConfigurator.UI.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConfigurator.DataAccess.DTOs.PriorityDtos;

namespace DbConfigurator.UI.Features.Priorities.Services
{
    public class PriorityService : GenericDataService<CreatePriorityDto, UpdatePriorityDto, Priority>, IPriorityService
    {
        public PriorityService(
            IDbConfiguratorApiClient client,
            AutoMapperConfig autoMapper)
            : base(client, autoMapper, "Priority")
        {
        }
    }
}
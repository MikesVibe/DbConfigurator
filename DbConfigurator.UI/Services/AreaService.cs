﻿using DbConfigurator.DataAccess.Repositories;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class AreaService : GenericDataService<Area, AreaDto, AreaRepository>
    {
        public AreaService(AreaRepository repository, AutoMapperConfig autoMapper)
            : base(repository, autoMapper)
        {
        }
    }
}

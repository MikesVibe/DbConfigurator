﻿using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.DTOs.CountryDtos;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Startup;

namespace DbConfigurator.UI.Features.Countries.Services
{
    public class CountryService : GenericDataService<CreateCountryDto, UpdateCountryDto, Country>, ICountryService
    {
        public CountryService(
            IDbConfiguratorApiClient client,
            AutoMapperConfig autoMapper)
            : base(client, autoMapper, "Country")
        {
        }
    }
}
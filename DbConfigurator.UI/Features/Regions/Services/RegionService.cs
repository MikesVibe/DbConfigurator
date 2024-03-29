﻿using DbConfigurator.DataAccess;
using DbConfigurator.DataAccess.DTOs.RegionDtos;
using DbConfigurator.Core.Models;
using DbConfigurator.UI.Base;
using DbConfigurator.UI.Base.Contracts;
using DbConfigurator.UI.Features.Areas.Services;
using DbConfigurator.UI.Features.BuisnessUnits.Services;
using DbConfigurator.UI.Features.Countries.Services;
using DbConfigurator.UI.Startup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Regions.Services
{
    public class RegionService : GenericDataService<CreateRegionDto, UpdateRegionDto, Region>, IRegionService
    {
        private readonly IAreaService _areaService;
        private readonly IBusinessUnitService _businessUnitService;
        private readonly ICountryService _countryService;
        private readonly AutoMapperConfig _autoMapper;

        public RegionService(
            IDbConfiguratorApiClient client,
            IAreaService areaService,
            IBusinessUnitService businessUnitService,
            ICountryService countryService,
            IStatusService statusService,
            AutoMapperConfig autoMapper)
        : base(client, statusService, autoMapper, "Region")
        {
            _areaService = areaService;
            _businessUnitService = businessUnitService;
            _countryService = countryService;
            _autoMapper = autoMapper;
        }

        public async Task<IEnumerable<Area>> GetAllAreasAsync()
        {
            return await _areaService.GetAllAsync();
        }

        public async Task<IEnumerable<BusinessUnit>> GetAllBusinessUnitsAsync()
        {
            return await _businessUnitService.GetAllAsync();

        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _countryService.GetAllAsync();
        }
        public override bool ChildrenHaveChanges()
        {
            return _areaService.HasChanges() || _businessUnitService.HasChanges() || _countryService.HasChanges() || base.ChildrenHaveChanges();
        }
    }
}

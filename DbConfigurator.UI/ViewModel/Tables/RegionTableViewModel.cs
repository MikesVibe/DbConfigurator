﻿using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.Windows;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class RegionTableViewModel : TableViewModelBase<RegionDtoWrapper>
    {
        private readonly AutoMapperConfig _autoMapper;
        private readonly Func<AddRegionViewModel> _addRegionCreator;
        private readonly IDataService _dataModel;

        public RegionTableViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            IDataService dataModel,
            AutoMapperConfig autoMapper,
            Func<AddRegionViewModel> addRegionCreator
            ) : base(eventAggregator, dialogService)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            _addRegionCreator = addRegionCreator;
            Items = new ObservableCollection<RegionDtoWrapper>();
        }

        public override async Task LoadAsync()
        {
            var regions = await _dataModel.GetAllRegionsAsync();
            foreach (var region in regions)
            {
                var mapped = _autoMapper.Mapper.Map<RegionDto>(region);
                var wrapped = new RegionDtoWrapper(mapped);
                Items.Add(wrapped);
            }
        }
        protected override async void OnAddExecute()
        {
            var regionViewModel = _addRegionCreator();
            await regionViewModel.LoadAsync();
            bool? result = DialogService.ShowDialog(regionViewModel);
            if (result == false || regionViewModel.Region is null)
                return;

            var region = regionViewModel.Region;
            var regionEntity = new Region
            {
                AreaId = region.Area.Id,
                BuisnessUnitId = region.BuisnessUnit.Id,
                CountryId = region.Country.Id,
            };

            _dataModel.Add(regionEntity);
            _dataModel.SaveChanges();

            var mapped = _autoMapper.Mapper.Map<RegionDto>(regionEntity);
            var wrapped = new RegionDtoWrapper(mapped);
            Items.Add(wrapped);
        }
        protected override async void OnEditExecute()
        {
            var regionViewModel = _addRegionCreator();
            regionViewModel.Region = SelectedItem;
            await regionViewModel.LoadAsync();
            bool? result = DialogService.ShowDialog(regionViewModel);
            if (result == false || regionViewModel.Region is null)
                return;

            var regionEntity = await _dataModel.RegionService.GetRegionByIdAsync(regionViewModel.Region.Id);

            //var area = _dataModel.GetAreaById(regionViewModel.Region.Area.Id);
            //var buisnessUnit = _dataModel.GetBuisnessUnitById(regionViewModel.Region.BuisnessUnit.Id);
            //var country = _dataModel.GetCountryById(regionViewModel.Region.Country.Id);

            var area = regionViewModel.Region.Area;
            var buisnessUnit = regionViewModel.Region.BuisnessUnit;
            var country = regionViewModel.Region.Country;

            if (regionEntity is null || area is null || buisnessUnit is null || country is null)
            {
                //log error message
                return;
            }
            regionEntity.Area = area;
            regionEntity.BuisnessUnit = buisnessUnit;
            regionEntity.Country = country;
            await _dataModel.SaveChangesAsync();

            SelectedItem!.Area = regionViewModel.Region.Area;
            SelectedItem!.BuisnessUnit = regionViewModel.Region.BuisnessUnit;
            SelectedItem!.Country = regionViewModel.Region.Country;
        }
        protected override async void OnRemoveExecute()
        {
            var regionEntity = await _dataModel.RegionService.GetRegionByIdAsync(SelectedItem!.Id);
            if (regionEntity is null )
                throw new ArgumentNullException(nameof(regionEntity));
            
            _dataModel.Remove(regionEntity);
            await _dataModel.SaveChangesAsync();
            Items.Remove(SelectedItem);
            SelectedItem = null;
        }
    }
}

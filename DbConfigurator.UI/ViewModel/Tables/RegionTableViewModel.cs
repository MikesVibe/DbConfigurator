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
        private readonly IRegionService _regionService;

        public RegionTableViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            IRegionService dataModel,
            AutoMapperConfig autoMapper,
            Func<AddRegionViewModel> addRegionCreator
            ) : base(eventAggregator, dialogService)
        {
            _regionService = dataModel;
            _autoMapper = autoMapper;
            _addRegionCreator = addRegionCreator;
            Items = new ObservableCollection<RegionDtoWrapper>();
        }

        public override async Task LoadAsync()
        {
            var regions = await _regionService.GetAllRegionsAsync();
            foreach (var region in regions)
            {
                var wrapped = new RegionDtoWrapper(region);
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

            var regionWrapper = regionViewModel.Region;
            if (regionWrapper is null || regionWrapper.Model is null)
                throw new ArgumentNullException();

            var region = await _regionService.AddAsync(regionWrapper.Model);

            Items.Add(new RegionDtoWrapper(region));
        }
        protected override async void OnEditExecute()
        {
            //var regionViewModel = _addRegionCreator();
            //regionViewModel.Region = SelectedItem;
            //await regionViewModel.LoadAsync();
            //bool? result = DialogService.ShowDialog(regionViewModel);
            //if (result == false || regionViewModel.Region is null)
            //    return;

            //var regionEntity = await _regionService.RegionService.GetRegionByIdAsync(regionViewModel.Region.Id);


            //var area = regionViewModel.Region.Area;
            //var buisnessUnit = regionViewModel.Region.BuisnessUnit;
            //var country = regionViewModel.Region.Country;

            //if (regionEntity is null || area is null || buisnessUnit is null || country is null)
            //{
            //    //log error message
            //    return;
            //}
            //regionEntity.Area = area;
            //regionEntity.BuisnessUnit = buisnessUnit;
            //regionEntity.Country = country;
            //await _regionService.SaveChangesAsync();

            //SelectedItem!.Area = regionViewModel.Region.Area;
            //SelectedItem!.BuisnessUnit = regionViewModel.Region.BuisnessUnit;
            //SelectedItem!.Country = regionViewModel.Region.Country;
        }
        protected override async void OnRemoveExecute()
        {
            //var regionEntity = await _regionService.RegionService.GetRegionByIdAsync(SelectedItem!.Id);
            //if (regionEntity is null )
            //    throw new ArgumentNullException(nameof(regionEntity));
            
            //_regionService.Remove(regionEntity);
            //await _regionService.SaveChangesAsync();
            //Items.Remove(SelectedItem);
            //SelectedItem = null;
        }
    }
}
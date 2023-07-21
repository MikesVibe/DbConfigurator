using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Detail;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class RegionTableViewModel : TableViewModelBase<RegionDtoWrapper, RegionDto, IRegionService>
    {
        private readonly IRegionService _regionService;

        public RegionTableViewModel(
            IEventAggregator eventAggregator,
            IWindowService dialogService,
            IRegionService dataService,
            AutoMapperConfig autoMapper,
            Func<RegionDetailViewModel> addRegionCreator
            ) : base(eventAggregator, dialogService, dataService, addRegionCreator)
        {
            _regionService = dataService;
            Items = new ObservableCollection<RegionDtoWrapper>();
        }

        public override async Task LoadAsync()
        {
            var regions = await _regionService.GetAllAsync();
            foreach (var region in regions)
            {
                var wrapped = new RegionDtoWrapper(region);
                Items.Add(wrapped);
            }
        }
        //protected override async void OnAddExecute()
        //{
        //    var regionViewModel = _addRegionCreator();
        //    await regionViewModel.LoadAsync();
        //    bool? result = WindowService.ShowWindow(regionViewModel);
        //    if (result == false || regionViewModel.Region is null)
        //        return;

        //    var regionWrapper = regionViewModel.Region;
        //    if (regionWrapper is null || regionWrapper.Model is null)
        //        throw new ArgumentNullException();

        //    var region = await _regionService.AddAsync(regionWrapper.Model);

        //    Items.Add(new RegionDtoWrapper(region));
        //}
        //protected override async void OnEditExecute()
        //{
        //    var regionViewModel = _addRegionCreator();
        //    regionViewModel.Region = SelectedItem;
        //    await regionViewModel.LoadAsync();
        //    bool? result = WindowService.ShowWindow(regionViewModel);
        //    if (result == false || regionViewModel.Region is null)
        //        return;

        //    var regionDto = regionViewModel.Region.Model;
        //    var status = await _regionService.UpdateAsync(regionDto);
        //    if (status == false)
        //        return;


        //    SelectedItem!.Area = regionDto.Area;
        //    SelectedItem!.BuisnessUnit = regionDto.BuisnessUnit;
        //    SelectedItem!.Country = regionDto.Country;
        //}

    }
}

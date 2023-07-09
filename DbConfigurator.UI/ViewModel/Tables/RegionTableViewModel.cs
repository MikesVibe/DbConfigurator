using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
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
            IRegionService dataService,
            AutoMapperConfig autoMapper,
            Func<AddRegionViewModel> addRegionCreator
            ) : base(eventAggregator, dialogService)
        {
            _regionService = dataService;
            _autoMapper = autoMapper;
            _addRegionCreator = addRegionCreator;
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
            var regionViewModel = _addRegionCreator();
            regionViewModel.Region = SelectedItem;
            await regionViewModel.LoadAsync();
            bool? result = DialogService.ShowDialog(regionViewModel);
            if (result == false || regionViewModel.Region is null)
                return;

            var regionDto = regionViewModel.Region.Model;
            var status = await _regionService.UpdateAsync(regionDto);
            if (status == false)
                return;


            SelectedItem!.Area = regionDto.Area;
            SelectedItem!.BuisnessUnit = regionDto.BuisnessUnit;
            SelectedItem!.Country = regionDto.Country;
        }
        protected override async void OnRemoveExecute()
        {
            await _regionService.RemoveByIdAsync(SelectedItem!.Id);

            Items.Remove(SelectedItem);
            SelectedItem = null;
        }
    }
}

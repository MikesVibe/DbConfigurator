using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Regions
{
    public class RegionTableViewModel : TableViewModelBase<RegionDtoWrapper, RegionDto, IRegionService>
    {

        public RegionTableViewModel(
            IEventAggregator eventAggregator,
            IWindowService dialogService,
            IRegionService dataService,
            AutoMapperConfig autoMapper,
            Func<RegionDetailViewModel> addRegionCreator
            ) : base(eventAggregator, dialogService, dataService, addRegionCreator)
        {
            EventAggregator.GetEvent<CreateRegionEvent>()
                .Subscribe(OnCreateExecute);
            EventAggregator.GetEvent<EditRegionEvent>()
                .Subscribe(OnEditExecute);
        }


        private void OnCreateExecute(CreateRegionEventArgs obj)
        {
            var wrapped = new RegionDtoWrapper(obj.Region);
            Items.Add(wrapped);
        }
        private void OnEditExecute(EditRegionEventArgs obj)
        {
            var region = Items.Where(a => a.Id == obj.Region.Id).FirstOrDefault();
            if (region is null)
            {
                RefreshItemsList();
                return;
            }

            var recipientDto = obj.Region;
            region.Country = recipientDto.Country;
            region.BuisnessUnit = recipientDto.BuisnessUnit;
            region.Area = recipientDto.Area;
        }

        public override async Task LoadAsync()
        {
            var regions = await DataService.GetAllAsync();
            foreach (var region in regions)
            {
                var wrapped = new RegionDtoWrapper(region);
                Items.Add(wrapped);
            }
        }
    }
}

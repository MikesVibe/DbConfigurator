using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Contracts;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Features.Areas
{
    public class AreaTableViewModel : TableViewModelBase<AreaDtoWrapper, AreaDto, IAreaService>, ITableViewModel
    {

        public AreaTableViewModel(
            IEventAggregator eventAggregator,
            IWindowService dialogService,
            IAreaService dataService,
            Func<AreaDetailViewModel> areaDetailViewModelCreator,
            AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService, dataService, areaDetailViewModelCreator, autoMapper)
        {
            EventAggregator.GetEvent<CreateAreaEvent>()
                .Subscribe(OnCreateAreaExecute);
            EventAggregator.GetEvent<EditAreaEvent>()
                .Subscribe(OnEditExecute);
        }


        public override async Task LoadAsync()
        {
            var areas = await DataService.GetAllAsync();
            foreach (var area in areas)
            {
                var wrapped = new AreaDtoWrapper(area);
                Items.Add(wrapped);
            }
        }

        private void OnCreateAreaExecute(CreateAreaEventArgs obj)
        {
            var wrapped = new AreaDtoWrapper(obj.Entity);
            Items.Add(wrapped);
        }
    }
}

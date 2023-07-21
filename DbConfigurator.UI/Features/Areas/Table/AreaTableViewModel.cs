using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Event;
using DbConfigurator.UI.Features.Areas.Event;
using DbConfigurator.UI.Services.Interfaces;
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
            Func<AreaDetailViewModel> areaDetailViewModelCreator)
            : base(eventAggregator, dialogService, dataService, areaDetailViewModelCreator)
        {
            EventAggregator.GetEvent<CreateAreaEvent>()
                .Subscribe(OnCreateAreaExecute);
            EventAggregator.GetEvent<EditAreaEvent>()
                .Subscribe(OnEditAreaExecute);
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
            var wrapped = new AreaDtoWrapper(obj.Area);
            Items.Add(wrapped);
        }
        private void OnEditAreaExecute(EditAreaEventArgs obj)
        {
            var area = Items.Where(a => a.Id == obj.Area.Id).FirstOrDefault();
            if (area is null)
            {
                RefreshItemsList();
                return;
            }

            area.Name = obj.Area.Name;
        }

    }
}

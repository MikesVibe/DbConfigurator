using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Detail;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class AreaTableViewModel : TableViewModelBase<AreaDtoWrapper, AreaDto, IAreaService>, ITableViewModel
    {
        private readonly Func<AreaDetailViewModel> _areaDetailViewModelCreator;
        public AreaTableViewModel(
            IEventAggregator eventAggregator,
            IWindowService dialogService,
            IAreaService dataService, 
            Func<AreaDetailViewModel> areaDetailViewModelCreator)
            : base(eventAggregator, dialogService, dataService)
        {
            _areaDetailViewModelCreator = areaDetailViewModelCreator;
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
        protected async override void OnAddExecute()
        {
            var addAreaViewModel = _areaDetailViewModelCreator();

            bool? result = DialogService.ShowDialog(addAreaViewModel);

            if (result == false)
                return;

            var areaDto = await DataService.AddAsync(addAreaViewModel.Area.Model);
            var wrapped = new AreaDtoWrapper(areaDto);
            Items.Add(wrapped);
        }
        protected async override void OnEditExecute()
        {
            var addAreaViewModel = _areaDetailViewModelCreator();
            addAreaViewModel.Area = new AreaDtoWrapper(SelectedItem!.Model);

            bool? result = DialogService.ShowDialog(addAreaViewModel);
            if (result == false)
                return;

            var status = await DataService.UpdateAsync(SelectedItem!.Model);

            SelectedItem.Name = SelectedItem!.Model.Name;
        }
    }
}

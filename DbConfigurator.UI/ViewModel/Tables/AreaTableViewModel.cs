using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Detail;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class AreaTableViewModel : TableViewModelBase<AreaDtoWrapper, AreaDto, IAreaService>, ITableViewModel
    {
        public AreaTableViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            IAreaService dataService)
            : base(eventAggregator, dialogService, dataService)
        {
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
            var addAreaViewModel = new AreaDetailViewModel();

            bool? result = DialogService.ShowDialog(addAreaViewModel);

            if (result == false)
                return;

            var areaDto = await DataService.AddAsync(addAreaViewModel.Area.Model);
            var wrapped = new AreaDtoWrapper(areaDto);
            Items.Add(wrapped);
        }
        protected async override void OnEditExecute()
        {
            var addAreaViewModel = new AreaDetailViewModel();
            addAreaViewModel.Area = new AreaDtoWrapper(SelectedItem!.Model);

            bool? result = DialogService.ShowDialog(addAreaViewModel);
            if (result == false)
                return;

            var status = await DataService.UpdateAsync(SelectedItem!.Model);

            SelectedItem.Name = SelectedItem!.Model.Name;
        }
    }
}

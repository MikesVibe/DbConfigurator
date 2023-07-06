using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class AreaTableViewModel : TableViewModelBase<AreaDtoWrapper>, ITableViewModel
    {
        private readonly AreaService _dataService;
        private readonly AutoMapperConfig _autoMapper;

        public AreaTableViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            AreaService dataService,
            AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService)
        {
            _dataService = dataService;
            _autoMapper = autoMapper;
        }

        public override async Task LoadAsync()
        {
            var areas = await _dataService.GetAllAsync();
            foreach (var area in areas)
            {
                var mapped = _autoMapper.Mapper.Map<AreaDto>(area);
                var wrapped = new AreaDtoWrapper(mapped);
                Items.Add(wrapped);
            }
        }
        protected async override void OnAddExecute()
        {
            var addAreaViewModel = new AddAreaViewModel();

            bool? result = DialogService.ShowDialog(addAreaViewModel);

            if (result == false)
                return;

            var areaDto = await _dataService.AddAsync(addAreaViewModel.Area.Model);
            var wrapped = new AreaDtoWrapper(areaDto);
            Items.Add(wrapped);
        }
        protected async override void OnEditExecute()
        {
            var addAreaViewModel = new AddAreaViewModel();
            addAreaViewModel.Area = new AreaDtoWrapper(SelectedItem!.Model);

            bool? result = DialogService.ShowDialog(addAreaViewModel);
            if (result == false)
                return;

            var areaDto = await _dataService.UpdateAsync(SelectedItem!.Model);

            SelectedItem.Name = areaDto.Name;
        }
        protected async override void OnRemoveExecute()
        {
            await _dataService.RemoveByIdAsync(SelectedItem!.Id);
    
            base.OnRemoveExecute();
        }
    }
}

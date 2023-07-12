using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Detail;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class BuisnessUnitTableViewModel : TableViewModelBase<BuisnessUnitDtoWrapper, BuisnessUnitDto, IBuisnessUnitService>, ITableViewModel
    {
        private readonly IBuisnessUnitService _dataService;
        private readonly AutoMapperConfig _autoMapper;

        public BuisnessUnitTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IBuisnessUnitService dataService, AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService, dataService)
        {
            _dataService = dataService;
            _autoMapper = autoMapper;
        }

        public override async Task LoadAsync()
        {
            var buisnessUnits = await _dataService.GetAllAsync();
            foreach (var buisnessUnit in buisnessUnits)
            {
                var wrapped = new BuisnessUnitDtoWrapper(buisnessUnit);
                Items.Add(wrapped);
            }
        }
        protected override void OnAddExecute()
        {
            var addbuisnessUnitViewModel = new BuisnessUnitDetailViewModel();

            bool? result = DialogService.ShowDialog(addbuisnessUnitViewModel);

            if (result == false)
                return;

            var buisnessUnitDto = _dataService.Add(addbuisnessUnitViewModel.BuisnessUnit.Model);
            var wrapped = new BuisnessUnitDtoWrapper(buisnessUnitDto);
            Items.Add(wrapped);
        }
        protected override void OnEditExecute()
        {
            var buisnessUnitDetailViewModel = new BuisnessUnitDetailViewModel();
            buisnessUnitDetailViewModel.BuisnessUnit = new BuisnessUnitDtoWrapper(SelectedItem!.Model);

            bool? result = DialogService.ShowDialog(buisnessUnitDetailViewModel);
            if (result == false)
                return;

            var status = _dataService.Update(SelectedItem!.Model);

            SelectedItem.Name = SelectedItem!.Model.Name;
        }

        protected override void OnSelectionChangedExecute()
        {
            base.OnSelectionChangedExecute();
        }
    }
}

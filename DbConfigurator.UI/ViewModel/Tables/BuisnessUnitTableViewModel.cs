using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Detail;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class BuisnessUnitTableViewModel : TableViewModelBase<BuisnessUnitDtoWrapper, BuisnessUnitDto, IBuisnessUnitService>, ITableViewModel
    {

        public BuisnessUnitTableViewModel(IEventAggregator eventAggregator, IWindowService dialogService, IBuisnessUnitService dataService, AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService, dataService)
        {
        }

        public override async Task LoadAsync()
        {
            var buisnessUnits = await DataService.GetAllAsync();
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

            var buisnessUnitDto = DataService.Add(addbuisnessUnitViewModel.BuisnessUnit.Model);
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

            var status = DataService.Update(SelectedItem!.Model);

            SelectedItem.Name = SelectedItem!.Model.Name;
        }
    }
}

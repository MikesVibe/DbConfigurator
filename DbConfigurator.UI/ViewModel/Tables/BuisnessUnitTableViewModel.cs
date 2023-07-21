using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Detail;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class BuisnessUnitTableViewModel : TableViewModelBase<BuisnessUnitDtoWrapper, BuisnessUnitDto, IBuisnessUnitService>, ITableViewModel
    {

        public BuisnessUnitTableViewModel(IEventAggregator eventAggregator,
            IWindowService dialogService,
            IBuisnessUnitService dataService,
            AutoMapperConfig autoMapper,
            Func<BuisnessUnitDetailViewModel> buisnessUnitDetailViewModelCreator)
            : base(eventAggregator, dialogService, dataService, buisnessUnitDetailViewModelCreator)
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
        //protected override void OnAddExecute()
        //{
        //    var buisnessUnitDetailVm = _buisnessUnitDetailViewModelCreator();
        //    WindowService.ShowWindow(buisnessUnitDetailVm);
        //    if (buisnessUnitDetailVm.WasCancelled == true)
        //        return;

        //    var wrapped = new BuisnessUnitDtoWrapper(buisnessUnitDetailVm.EntityDto!);
        //    Items.Add(wrapped);
        //}
        //protected override void OnEditExecute()
        //{
        //    var buisnessUnitDetailViewModel = _buisnessUnitDetailViewModelCreator();
        //    buisnessUnitDetailViewModel.BuisnessUnit = new BuisnessUnitDtoWrapper(SelectedItem!.Model);
        //    WindowService.ShowWindow(buisnessUnitDetailViewModel);
        //    if (buisnessUnitDetailViewModel.WasCancelled == true)
        //        return;


        //    SelectedItem.Name = SelectedItem!.Model.Name;
        //}
    }
}

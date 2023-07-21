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
        public AreaTableViewModel(
            IEventAggregator eventAggregator,
            IWindowService dialogService,
            IAreaService dataService, 
            Func<AreaDetailViewModel> areaDetailViewModelCreator)
            : base(eventAggregator, dialogService, dataService, areaDetailViewModelCreator)
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
        //protected override void OnAddExecute()
        //{
        //    var areaDetialVm = _areaDetailViewModelCreator();
        //    WindowService.ShowWindow(areaDetialVm);
        //    if (areaDetialVm.WasCancelled == false)
        //        return;

        //    var wrapped = new AreaDtoWrapper(areaDetialVm.EntityDto!);
        //    Items.Add(wrapped);
        //}
        //protected override void OnEditExecute()
        //{
        //    var areaDetialVm = _areaDetailViewModelCreator();
        //    areaDetialVm.Area = new AreaDtoWrapper(SelectedItem!.Model);
        //    WindowService.ShowWindow(areaDetialVm);
        //    if (areaDetialVm.WasCancelled == false)
        //        return;


        //    SelectedItem.Name = SelectedItem!.Model.Name;
        //}
    }
}

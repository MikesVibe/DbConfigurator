using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Wrapper;
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

namespace DbConfigurator.UI.Features.BuisnessUnits
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
            EventAggregator.GetEvent<CreateBuisnessUnitEvent>()
                .Subscribe(OnCreateExecute);
            EventAggregator.GetEvent<EditBuisnessUnitEvent>()
                .Subscribe(OnEditExecute);
        }


        private void OnCreateExecute(CreateBuisnessUnitEventArgs obj)
        {
            var wrapped = new BuisnessUnitDtoWrapper(obj.BuisnessUnit);
            Items.Add(wrapped);
        }
        private void OnEditExecute(EditBuisnessUnitEventArgs obj)
        {
            var buisnessUnit = Items.Where(a => a.Id == obj.BuisnessUnit.Id).FirstOrDefault();
            if (buisnessUnit is null)
            {
                RefreshItemsList();
                return;
            }

            var buisnessUnitDto = obj.BuisnessUnit;
            buisnessUnit.Name = buisnessUnitDto.Name;
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
    }
}

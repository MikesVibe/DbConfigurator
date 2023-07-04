using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class BuisnessUnitTableViewModel : TableViewModelBase<BuisnessUnitDtoWrapper>, ITableViewModel
    {
        private readonly IDataService _dataModel;
        private readonly AutoMapperConfig _autoMapper;

        public BuisnessUnitTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IDataService dataModel, AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
        }

        public override async Task LoadAsync()
        {
            var buisnessUnits = await _dataModel.GetAllBuisnessUnitsAsync();
            foreach (var buisnessUnit in buisnessUnits)
            {
                var mapped = _autoMapper.Mapper.Map<BuisnessUnitDto>(buisnessUnit);
                var wrapped = new BuisnessUnitDtoWrapper(mapped);
                Items.Add(wrapped);
            }
        }
        protected override void OnAddExecute()
        {
            var addbuisnessUnitViewModel = new AddBuisnessUnitViewModel();

            bool? result = DialogService.ShowDialog(addbuisnessUnitViewModel);

            if (result == false)
                return;

            var buisnessUnit = _autoMapper.Mapper.Map<BuisnessUnit>(addbuisnessUnitViewModel.BuisnessUnit.Model);

            _dataModel.Add(buisnessUnit);
            _dataModel.SaveChanges();
            var mapped = _autoMapper.Mapper.Map<BuisnessUnitDto>(buisnessUnit);
            var wrapped = new BuisnessUnitDtoWrapper(mapped);
            Items.Add(wrapped);
        }
        protected override void OnEditExecute()
        {
            var buisnessUnitDto = _autoMapper.Mapper.Map<BuisnessUnitDto>(SelectedItem!.Model);
            var buisnessUnitViewModel = new AddBuisnessUnitViewModel(buisnessUnitDto);

            bool? result = DialogService.ShowDialog(buisnessUnitViewModel);

            if (result == false)
                return;

            var buisnessUnitEntity = _dataModel.GetBuisnessUnitById(SelectedItem!.Id);
            if (buisnessUnitEntity is null)
            {
                //Log some error
                return;
            }

            _autoMapper.Mapper.Map(buisnessUnitViewModel.BuisnessUnit.Model, buisnessUnitEntity);

            _dataModel.SaveChanges();
            SelectedItem.Name = buisnessUnitEntity.Name;
        }
        protected override void OnRemoveExecute()
        {
            if (SelectedItem is null)
                return;

            var buisnessUnit = _dataModel.GetBuisnessUnitById(SelectedItem.Id);
            if (buisnessUnit is null)
            {
                //Log some error mesage here
                return;
            }

            _dataModel.Remove(buisnessUnit);
            _dataModel.SaveChanges();
            base.OnRemoveExecute();
        }
        protected override void OnSelectionChangedExecute()
        {
            base.OnSelectionChangedExecute();
        }
    }
}

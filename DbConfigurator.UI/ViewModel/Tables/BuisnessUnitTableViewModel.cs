﻿using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
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
        private readonly ICombinedDataService _dataService;
        private readonly AutoMapperConfig _autoMapper;

        public BuisnessUnitTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ICombinedDataService dataService, AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService)
        {
            _dataService = dataService;
            _autoMapper = autoMapper;
        }

        public override async Task LoadAsync()
        {
            var buisnessUnits = await _dataService.GetAllBuisnessUnitsAsync();
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

            _dataService.Add(buisnessUnit);
            _dataService.SaveChanges();
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

            var buisnessUnitEntity = _dataService.GetBuisnessUnitById(SelectedItem!.Id);
            if (buisnessUnitEntity is null)
            {
                //Log some error
                return;
            }

            _autoMapper.Mapper.Map(buisnessUnitViewModel.BuisnessUnit.Model, buisnessUnitEntity);

            _dataService.SaveChanges();
            SelectedItem.Name = buisnessUnitEntity.Name;
        }
        protected override void OnRemoveExecute()
        {
            if (SelectedItem is null)
                return;

            var buisnessUnit = _dataService.GetBuisnessUnitById(SelectedItem.Id);
            if (buisnessUnit is null)
            {
                //Log some error mesage here
                return;
            }

            _dataService.Remove(buisnessUnit);
            _dataService.SaveChanges();
            base.OnRemoveExecute();
        }
        protected override void OnSelectionChangedExecute()
        {
            base.OnSelectionChangedExecute();
        }
    }
}

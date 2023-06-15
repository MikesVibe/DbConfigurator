﻿using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.Model.Entities.Table;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class BuisnessUnitTableViewModel : TableViewModelBase, ITabelViewModel
    {
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;
        private IDialogService _dialogService;


        public BuisnessUnitTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IDataModel dataModel, AutoMapperConfig autoMapper)
            : base(eventAggregator)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            _dialogService = dialogService;
        }

        public override async Task LoadAsync()
        {
            var buisnessUnits = await _dataModel.GetAllBuisnessUnitsAsync();
            foreach (var buisnessUnit in buisnessUnits)
            {
                var mapped = _autoMapper.Mapper.Map<BuisnessUnitTableItem>(buisnessUnit);
                Items.Add(mapped);
            }
        }

        protected override void OnAddExecute()
        {
            var addbuisnessUnitViewModel = new AddBuisnessUnitViewModel();

            bool? result = _dialogService.ShowDialog(addbuisnessUnitViewModel);

            if (result == false)
                return;

            string buisnessUnitName = addbuisnessUnitViewModel.BuisnessUnit.Name;
            var buisnessUnit = new BuisnessUnit
            {
                Name = buisnessUnitName
            };
            _dataModel.Add(buisnessUnit);
            _dataModel.SaveChanges();
            var mapped = _autoMapper.Mapper.Map<BuisnessUnitTableItem>(buisnessUnit);
            Items.Add(mapped);
        }

        protected override void OnRemoveExecute()
        {
            if (SelectedItem is null)
                return;
            //FIX here: diferent method from _dataModel needed
            //var buisnessUnit = _dataModel.GetAreaById(SelectedItem.Id);
            //if (buisnessUnit is null)
            //{
            //    //Log some error mesage here
            //    return;
            //}

            //_dataModel.Remove(buisnessUnit);
            //_dataModel.SaveChanges();
            //base.OnRemoveExecute();
        }

        protected override void OnSelectionChangedExecute()
        {
            base.OnSelectionChangedExecute();
        }
    }
}
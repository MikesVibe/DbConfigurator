using DbConfigurator.Model;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
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

        public ObservableCollection<BuisnessUnitDto> BuisnessUnits { get; set; } = new();

        public BuisnessUnitDto? SelectedBuisnessUnit { get; set; }


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
                var mapped = _autoMapper.Mapper.Map<BuisnessUnitDto>(buisnessUnit);
                BuisnessUnits.Add(mapped);
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
            var mapped = _autoMapper.Mapper.Map<BuisnessUnitDto>(buisnessUnit);
            BuisnessUnits.Add(mapped);
        }

        protected override void OnRemoveExecute()
        {
            if (SelectedBuisnessUnit is null)
                return;

            var buisnessUnit = _dataModel.GetAreaById(SelectedBuisnessUnit.Id);
            if (buisnessUnit is null)
            {
                //Log some error mesage here
                return;
            }

            BuisnessUnits.Remove(SelectedBuisnessUnit);
            _dataModel.Remove(buisnessUnit);
            _dataModel.SaveChanges();
            SelectedBuisnessUnit = null;
        }

        protected override bool OnRemoveCanExecute()
        {
            return SelectedBuisnessUnit is not null;
        }

        protected override void OnSelectionChangedExecute()
        {
            base.OnSelectionChangedExecute();
        }
    }
}

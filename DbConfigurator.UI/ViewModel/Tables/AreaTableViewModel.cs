using DbConfigurator.Model;
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
    public class AreaTableViewModel : TableViewModelBase, ITableViewModel
    {
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;
        private IDialogService _dialogService;

        public AreaTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IDataModel dataModel, AutoMapperConfig autoMapper)
            : base(eventAggregator)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            _dialogService = dialogService;
        }

        public override async Task LoadAsync()
        {
            var areas = await _dataModel.GetAllAreasAsync();
            foreach (var area in areas)
            {
                var mapped = _autoMapper.Mapper.Map<AreaTableItem>(area);
                Items.Add(mapped);
            }
        }

        protected override void OnAddExecute()
        {
            var addAreaViewModel = new AddAreaViewModel();

            bool? result = _dialogService.ShowDialog(addAreaViewModel);

            if (result == false)
                return;

            string areaName = addAreaViewModel.Area.Name;
            var area = new Area
            {
                Name = areaName
            };

            _dataModel.Add(area);
            _dataModel.SaveChanges();
            var mapped = _autoMapper.Mapper.Map<AreaTableItem>(area);
            Items.Add(mapped);
        }


        protected override void OnRemoveExecute()
        {
            if (SelectedItem is null)
                return;

            var area = _dataModel.GetAreaById(SelectedItem.Id);
            if (area is null)
            {
                //Log some error mesage here
                return;
            }

            _dataModel.Remove(area);
            _dataModel.SaveChanges();

            base.OnRemoveExecute();
        }

    }
}

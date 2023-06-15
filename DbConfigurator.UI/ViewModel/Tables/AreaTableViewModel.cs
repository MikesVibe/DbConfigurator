using DbConfigurator.Model;
using DbConfigurator.Model.DTOs;
using DbConfigurator.Model.Entities;
using DbConfigurator.UI.Services;
using DbConfigurator.UI.Startup;
using DbConfigurator.UI.ViewModel.Add;
using DbConfigurator.UI.ViewModel.Base;
using DbConfigurator.UI.ViewModel.Interfaces;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbConfigurator.UI.ViewModel.Tables
{
    public class AreaTableViewModel : TableViewModelBase, ITabelViewModel
    {
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;
        private IDialogService _dialogService;

        public ObservableCollection<AreaDto> Areas { get; set; } = new();
        //public ICommand AreaDoubleClickedCommand { get; set; }
        public AreaDto? SelectedArea { get; set; }



        public AreaTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IDataModel dataModel, AutoMapperConfig autoMapper)
            : base(eventAggregator)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
            _dialogService = dialogService;

            //AreaDoubleClickedCommand = new DelegateCommand(OnAreaDoubleClickedExecute);
        }

        public override async Task LoadAsync()
        {
            var areas = await _dataModel.GetAllAreasAsync();
            foreach (var area in areas)
            {
                var mapped = _autoMapper.Mapper.Map<AreaDto>(area);
                Areas.Add(mapped);
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
            var mapped = _autoMapper.Mapper.Map<AreaDto>(area);
            Areas.Add(mapped);
        }

        protected override bool OnRemoveCanExecute()
        {
            return SelectedArea != null;
        }

        protected override void OnRemoveExecute()
        {
            if (SelectedArea == null)
                return;

            var area = _dataModel.GetAreaById(SelectedArea.Id);
            Areas.Remove(SelectedArea);
            _dataModel.Remove(area!);
            _dataModel.SaveChanges();
            SelectedArea = null;
        }

    }
}

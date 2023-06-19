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
    public class AreaTableViewModel : TableViewModelBase<AreaDtoWrapper>, ITableViewModel
    {
        private readonly IDataModel _dataModel;
        private readonly AutoMapperConfig _autoMapper;

        public AreaTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IDataModel dataModel, AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService)
        {
            _dataModel = dataModel;
            _autoMapper = autoMapper;
        }

        public override async Task LoadAsync()
        {
            var areas = await _dataModel.GetAllAreasAsync();
            foreach (var area in areas)
            {
                var mapped = _autoMapper.Mapper.Map<AreaDto>(area);
                var wrapped = new AreaDtoWrapper(mapped);
                Items.Add(wrapped);
            }
        }

        protected override void OnAddExecute()
        {
            var addAreaViewModel = new AddAreaViewModel();

            bool? result = DialogService.ShowDialog(addAreaViewModel);

            if (result == false)
                return;

            var area = _autoMapper.Mapper.Map<Area>(addAreaViewModel.Area.Model);

            _dataModel.Add(area);
            _dataModel.SaveChanges();
            var mapped = _autoMapper.Mapper.Map<AreaDto>(area);
            var wrapped = new AreaDtoWrapper(mapped);
            Items.Add(wrapped);
        }

        protected override void OnEditExecute()
        {
            var areaDto = _autoMapper.Mapper.Map<AreaDto>(SelectedItem!.Model);
            var addAreaViewModel = new AddAreaViewModel(areaDto);

            bool? result = DialogService.ShowDialog(addAreaViewModel);

            if (result == false)
                return;

            var areaEntity = _dataModel.GetAreaById(SelectedItem!.Id);
            _autoMapper.Mapper.Map(addAreaViewModel.Area.Model, areaEntity);

            _dataModel.SaveChanges();
            _autoMapper.Mapper.Map(areaEntity, SelectedItem);
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

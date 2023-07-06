using DbConfigurator.Model.DTOs.Core;
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
    public class AreaTableViewModel : TableViewModelBase<AreaDtoWrapper>, ITableViewModel
    {
        private readonly ICombinedDataService _dataService;
        private readonly AutoMapperConfig _autoMapper;

        public AreaTableViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ICombinedDataService dataModel, AutoMapperConfig autoMapper)
            : base(eventAggregator, dialogService)
        {
            _dataService = dataModel;
            _autoMapper = autoMapper;
        }

        public override async Task LoadAsync()
        {
            var areas = await _dataService.GetAllAreasAsync();
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

            _dataService.Add(area);
            _dataService.SaveChanges();
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

            var areaEntity = _dataService.GetAreaById(SelectedItem!.Id);
            if (areaEntity is null)
            {
                //Log some error
                return;
            }
            _autoMapper.Mapper.Map(addAreaViewModel.Area.Model, areaEntity);

            _dataService.SaveChanges();
            SelectedItem.Name = areaEntity.Name;
        }
        protected override void OnRemoveExecute()
        {
            if (SelectedItem is null)
                return;

            var area = _dataService.GetAreaById(SelectedItem.Id);
            if (area is null)
            {
                //Log some error mesage here
                return;
            }

            _dataService.Remove(area);
            _dataService.SaveChanges();

            base.OnRemoveExecute();
        }
    }
}

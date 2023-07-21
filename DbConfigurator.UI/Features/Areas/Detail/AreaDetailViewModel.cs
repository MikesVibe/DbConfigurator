using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel;
using DbConfigurator.UI.ViewModel.Base;
using System.ComponentModel;

namespace DbConfigurator.UI.Features.Areas
{
    public class AreaDetailViewModel : DetailViewModelBase<IAreaService, AreaDto>, IDetailViewModel, INotifyPropertyChanged
    {
        public AreaDetailViewModel(IAreaService areaService) : base(areaService)
        {
            AreaDto areaDto = new();
            Area = new(areaDto);
            Area.Name = "";
            Title = "Area";
            ViewWidth = 560;
            ViewHeight = 340;
        }
        //public AreaDetailViewModel(AreaDto areaDto)
        //{
        //    Area = new(areaDto);
        //}

        public AreaDtoWrapper Area { get; set; }
    }
}

using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.ViewModel.Base;
using System.ComponentModel;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddAreaViewModel : EditingViewModelBase, IEditingViewModel, INotifyPropertyChanged
    {
        public AreaDtoWrapper Area { get; set; }

        public AddAreaViewModel()
        {
            AreaDto areaDto = new();
            Area = new(areaDto);
            Area.Name = "";
        }
        public AddAreaViewModel(AreaDto areaDto)
        {
            Area = new(areaDto);
        }
    }
}

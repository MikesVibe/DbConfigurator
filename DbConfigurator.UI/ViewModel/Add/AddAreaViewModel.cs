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
            AreaDto area = new();
            Area = new(area);
            Area.Name = "";

        }

    }
}

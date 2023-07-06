using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.ViewModel.Base;
using System.ComponentModel;

namespace DbConfigurator.UI.ViewModel.Add
{
    public class AddBuisnessUnitViewModel : EditingViewModelBase, IEditingViewModel, INotifyPropertyChanged
    {
        public AddBuisnessUnitViewModel()
        {
            BuisnessUnitDto buisnessUnit = new();
            BuisnessUnit = new(buisnessUnit);
            BuisnessUnit.Name = "";
        }
        public AddBuisnessUnitViewModel(BuisnessUnitDto buisnessUnitDto)
        {
            BuisnessUnit = new(buisnessUnitDto);
        }

        protected override bool CanAdd()
        {
            return true;
        }

        public BuisnessUnitDtoWrapper BuisnessUnit { get; set; }
    }
}

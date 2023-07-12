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
            Title = "BuisnessUnit";
            ViewWidth = 560;
            ViewHeight = 340;
        }
        public AddBuisnessUnitViewModel(BuisnessUnitDto buisnessUnitDto)
        {
            BuisnessUnit = new(buisnessUnitDto);
        }

        protected override bool OnAddCanExecute()
        {
            return true;
        }

        public BuisnessUnitDtoWrapper BuisnessUnit { get; set; }
    }
}

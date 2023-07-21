using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.DTOs.Wrapper;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.ViewModel.Base;
using System.ComponentModel;

namespace DbConfigurator.UI.ViewModel.Detail
{
    public class BuisnessUnitDetailViewModel : DetailViewModelBase<IBuisnessUnitService, BuisnessUnitDto>, IDetailViewModel, INotifyPropertyChanged
    {
        public BuisnessUnitDetailViewModel(IBuisnessUnitService buisnessUnitService) : base(buisnessUnitService)
        {
            BuisnessUnitDto buisnessUnit = new();
            BuisnessUnit = new(buisnessUnit);
            BuisnessUnit.Name = "";
            Title = "BuisnessUnit";
            ViewWidth = 560;
            ViewHeight = 340;
        }
        //public BuisnessUnitDetailViewModel(BuisnessUnitDto buisnessUnitDto)
        //{
        //    BuisnessUnit = new(buisnessUnitDto);
        //}

        protected override bool OnAddCanExecute()
        {
            return true;
        }

        public BuisnessUnitDtoWrapper BuisnessUnit { get; set; }
    }
}

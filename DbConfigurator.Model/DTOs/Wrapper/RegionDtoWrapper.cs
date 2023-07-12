using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Wrapper;

namespace DbConfigurator.Model.DTOs.Wrapper
{
    public class RegionDtoWrapper : ModelWrapper<RegionDto>, IWrapperWithId
    {
        public RegionDtoWrapper(RegionDto model) : base(model)
        {
        }
        public int Id
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }
        public AreaDto Area
        {
            get { return GetValue<AreaDto>(); }
            set
            {
                SetValue(value);
            }
        }
        public BuisnessUnitDto BuisnessUnit
        {
            get { return GetValue<BuisnessUnitDto>(); }
            set
            {
                SetValue(value);
            }
        }
        public CountryDto Country
        {
            get { return GetValue<CountryDto>(); }
            set
            {
                SetValue(value);
            }
        }
    }
}

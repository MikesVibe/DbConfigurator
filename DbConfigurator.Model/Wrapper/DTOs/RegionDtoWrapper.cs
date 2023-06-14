using DbConfigurator.Model.DTOs;

namespace DbConfigurator.Model.Wrapper.DTOs
{
    public class RegionDtoWrapper : ModelWrapper<RegionDto>
    {
        public RegionDtoWrapper(RegionDto model) : base(model)
        {
        }
        public int Id
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue<int>(value);
            }
        }
        public AreaDto Area
        {
            get { return GetValue<AreaDto>(); }
            set
            {
                SetValue<AreaDto>(value);
            }
        }
        public BuisnessUnitDto BuisnessUnit
        {
            get { return GetValue<BuisnessUnitDto>(); }
            set
            {
                SetValue<BuisnessUnitDto>(value);
            }
        }
        public CountryDto Country
        {
            get { return GetValue<CountryDto>(); }
            set
            {
                SetValue<CountryDto>(value);
            }
        }
    }
}

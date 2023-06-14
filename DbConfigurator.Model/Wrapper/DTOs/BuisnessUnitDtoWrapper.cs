using DbConfigurator.Model.DTOs;

namespace DbConfigurator.Model.Wrapper.DTOs
{
    public class BuisnessUnitDtoWrapper : ModelWrapper<BuisnessUnitDto>
    {
        public BuisnessUnitDtoWrapper(BuisnessUnitDto model) : base(model)
        {
        }
        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue<string>(value); }
        }
    }
}

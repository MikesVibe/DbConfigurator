using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Wrapper;

namespace DbConfigurator.Model.DTOs.Wrapper
{
    public class BuisnessUnitDtoWrapper : ModelWrapper<BuisnessUnitDto>
    {
        public BuisnessUnitDtoWrapper(BuisnessUnitDto model) : base(model)
        {
        }
        public int Id
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}

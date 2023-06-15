using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Wrapper;

namespace DbConfigurator.Model.DTOs.Wrapper
{
    public class AreaDtoWrapper : ModelWrapper<AreaDto>
    {
        public AreaDtoWrapper(AreaDto model) : base(model)
        {
        }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}

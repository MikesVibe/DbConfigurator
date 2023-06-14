using DbConfigurator.Model.DTOs;

namespace DbConfigurator.Model.Wrapper.DTOs
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

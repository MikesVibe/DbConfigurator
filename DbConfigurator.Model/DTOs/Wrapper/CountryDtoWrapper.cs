using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Wrapper;

namespace DbConfigurator.Model.DTOs.Wrapper
{
    public class CountryDtoWrapper : ModelWrapper<CountryDto>
    {
        public CountryDtoWrapper(CountryDto model) : base(model)
        {
        }
        public int Id
        {
            get { return GetValue<int>(); }
        }
        public string CountryName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CountryCode
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}

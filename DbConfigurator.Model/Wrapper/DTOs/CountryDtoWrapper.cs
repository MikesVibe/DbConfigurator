using DbConfigurator.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.Wrapper.DTOs
{
    public class CountryDtoWrapper : ModelWrapper<CountryDto>
    {
        public CountryDtoWrapper(CountryDto model) : base(model)
        {
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

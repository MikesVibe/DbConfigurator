using DbConfigurator.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

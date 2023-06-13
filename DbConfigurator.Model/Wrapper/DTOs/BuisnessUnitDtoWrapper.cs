using DbConfigurator.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

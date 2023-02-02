using DbConfigurator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Wrapper
{
    public class DistributionInfoLookupWrapper : ModelWrapper<DistributionInfoLookup>
    {
        public DistributionInfoLookupWrapper(DistributionInfoLookup model) : base(model)
        {
        }
        public string Area
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string BuisnessUnit
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Country
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Priority
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string TO
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CC
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}

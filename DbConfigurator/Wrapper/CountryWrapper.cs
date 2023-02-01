using DbConfigurator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Wrapper
{
    public class CountryWrapper : ModelWrapper<Country>
    {
        public CountryWrapper(Country model) : base(model)
        {
        }

        public int Id
        {
            get { return GetValue<int>(); }
        }
        public string Name
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue<string>(value);
            }
        }
        public string ShortCode
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue<string>(value);
            }
        }
        public int BuisnessUnitId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue<int>(value);
            }
        }
        public BuisnessUnit BuisnessUnit
        {
            get { return GetValue<BuisnessUnit>(); }
            set
            {
                SetValue<BuisnessUnit>(value);
            }
        }
    }
}

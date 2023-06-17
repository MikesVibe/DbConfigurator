using DbConfigurator.Model.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.Entities.Wrapper.Table
{
    public class CountryTableItemWrapper : ModelWrapper<CountryTableItem>, ITableItem
    {
        public CountryTableItemWrapper(CountryTableItem model) : base(model)
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
            set
            {
                SetValue(value);
            }
        }
        public string ShortCode
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }
    }
}

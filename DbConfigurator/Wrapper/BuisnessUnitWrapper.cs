using DbConfigurator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Wrapper
{
    public class BuisnessUnitWrapper : ModelWrapper<BuisnessUnit>
    {
        public BuisnessUnitWrapper(BuisnessUnit model) : base(model)
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
        public int AreaId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue<int>(value);
            }
        }
        public ICollection<Country> Countries
        {
            get { return GetValue<ICollection<Country>>(); }
            set
            {
                SetValue<ICollection<Country>>(value);
            }
        }
        public ICollection<DistributionInformation> DistributionInformations
        {
            get { return GetValue<ICollection<DistributionInformation>>(); }
            set
            {
                SetValue<ICollection<DistributionInformation>>(value);
            }
        }

    }
}

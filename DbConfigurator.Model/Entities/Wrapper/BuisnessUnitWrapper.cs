using DbConfigurator.Model.Entities.Core;
using System.Collections.Generic;

namespace DbConfigurator.Model.Entities.Wrapper
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
            set { SetValue(value); }
        }
        public int AreaId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }
        public Area Area
        {
            get { return GetValue<Area>(); }
            set { SetValue(value); }
        }
        public ICollection<Country> Countries
        {
            get { return GetValue<ICollection<Country>>(); }
            set
            {
                SetValue(value);
            }
        }
        public ICollection<DistributionInformation> DistributionInformations
        {
            get { return GetValue<ICollection<DistributionInformation>>(); }
            set
            {
                SetValue(value);
            }
        }

    }
}

using DbConfigurator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Wrapper
{
    public class DistributionInformationWrapper : ModelWrapper<DistributionInformation>
    {
        public DistributionInformationWrapper(DistributionInformation model) : base(model)
        {
        }

        public int Id 
        { 
            get { return GetValue<int>(); }
        }

        public int BusinessUnitId
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

        public int PriorityId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue<int>(value);
            }
        }
        public Priority Priority
        {
            get { return GetValue<Priority>(); }
            set
            {
                SetValue<Priority>(value);
            }
        }

        public ICollection<RecipientsGroup> RecipientsGroup_Collection
        {
            get { return GetValue<ICollection<RecipientsGroup>>(); }
            set
            {
                SetValue<ICollection<RecipientsGroup>>(value);
            }
        }
    }
}

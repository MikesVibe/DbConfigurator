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
            TO = new List<string>();
            CC = new List<string>();
            //BuisnessUnit = new BuisnessUnitWrapper(model.BuisnessUnit);
            //Priority = new PriorityWrapper(model.Priority);
            //Country = new CountryWrapper(model.Country);'
            Priority = new PriorityWrapper(model.Priority);
        }
        public DistributionInformationWrapper() : base(new DistributionInformation())
        {
        }
        public int Id 
        { 
            get { return GetValue<int>(); }
        }

        public int CountryId
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue<int>(value);
            }
        }
        public Country Country
        {
            get { return GetValue<Country>(); }
            set
            {
                SetValue<Country>(value);
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
        public Priority Priority { get; set; }

        public ICollection<RecipientsGroup> RecipientsGroup_Collection
        {
            get { return GetValue<ICollection<RecipientsGroup>>(); }
            set
            {
                SetValue<ICollection<RecipientsGroup>>(value);
            }
        }

        public ICollection<string> TO { get; set; }
        public ICollection<string> CC { get; set; }

    }
}

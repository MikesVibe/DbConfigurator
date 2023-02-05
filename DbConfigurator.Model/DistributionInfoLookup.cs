using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class DistributionInfoLookup
    {
        public DistributionInfoLookup(DistributionInformation disInfo)
        {
            Id = disInfo.Id;
            Area = disInfo.Country.BuisnessUnit.Area.Name;
            BuisnessUnit = disInfo.Country.BuisnessUnit.Name;
            Country = disInfo.Country.Name;
            Priority = disInfo.Priority.Name;

            //var To = disInfo.RecipientsGroup_Collection.Where(g => g.DestinationField.Name == "TO").FirstOrDefault();
            //TO = To?.Recipients.Select(r => r.Email).ToList() ?? new List<string>();


        }

        public int Id { get; set; }
        public string Area { get; set; }
        public string BuisnessUnit { get; set; }
        public string Country { get; set; }
        public string Priority { get; set; }
        public ICollection<string> TO { get; set; }
        public ICollection<string> CC { get; set; }
    }
}

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
            _disInfo = disInfo;

            Id = disInfo.Id;
            Area = disInfo.Country.BuisnessUnit.Area.Name;
            AreaId = disInfo.Country.BuisnessUnit.Area.Id;
            BuisnessUnit = disInfo.Country.BuisnessUnit.Name;
            BuisnessUnitId = disInfo.Country.BuisnessUnit.Id;
            Country = disInfo.Country.Name;
            CountryId = disInfo.Country.Id;
            Priority = disInfo.Priority.Name;
            PriorityId = disInfo.Priority.Id;

            var To = disInfo.RecipientsGroup_Collection.Where(g => g.DestinationField.Id == 1).FirstOrDefault();
            var Cc = disInfo.RecipientsGroup_Collection.Where(g => g.DestinationField.Id == 2).FirstOrDefault();
            if (To != null)
                TO = To.Recipients.Select(r => r.Email).ToList();
            if (Cc != null)
                CC = Cc.Recipients.Select(r => r.Email).ToList();

        }

        private DistributionInformation _disInfo;

        public int Id { get; set; }
        public string Area { get; set; }
        public int AreaId { get; set; }
        public string BuisnessUnit { get; set; }
        public int BuisnessUnitId { get; set; }
        public string Country { get; set; }
        public int CountryId 
        { 
            get { return _disInfo.CountryId; }
            
            set 
            {
                _disInfo.CountryId = value;
            }
        }
        public string Priority { get; set; }
        public int PriorityId { get; set; }
        public ICollection<string> TO { get; set; }
        public ICollection<string> CC { get; set; }
    }
}

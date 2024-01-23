using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Core.Models
{
    public class DistributionList
    {
        public DistributionList(IEnumerable<DistributionInformation> matchingDisInfoByPriority)
        {
            SelectedDistributionInformationIds = matchingDisInfoByPriority.Select(d => d.Id).ToList();

            foreach (var disfInfo in matchingDisInfoByPriority)
            {
                RecipientsTo.AddRange(disfInfo.RecipientsTo);
                RecipientsCc.AddRange(disfInfo.RecipientsCc);
            }
        }

        public List<int> SelectedDistributionInformationIds { get; set; } = new List<int>();
        public List<Recipient> RecipientsTo { get; set; } = new List<Recipient>();
        public List<Recipient> RecipientsCc { get; set; } = new List<Recipient>();

    }
}

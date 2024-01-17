using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.Entities.Core
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
        public List<Model.Entities.Core.Recipient> RecipientsTo { get; set; } = new List<Model.Entities.Core.Recipient>();
        public List<Model.Entities.Core.Recipient> RecipientsCc { get; set; } = new List<Model.Entities.Core.Recipient>();

    }
}

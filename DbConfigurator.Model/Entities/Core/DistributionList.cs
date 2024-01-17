using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.Entities.Core
{
    public class DistributionList
    {
        public List<Model.Entities.Core.Recipient> RecipientsTo { get; set; } = new List<Model.Entities.Core.Recipient>();
        public List<Model.Entities.Core.Recipient> RecipientsCc { get; set; } = new List<Model.Entities.Core.Recipient>();

    }
}

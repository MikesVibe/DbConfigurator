using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConfigurator.Model.Entities;

namespace DbConfigurator.Model
{
    public class RecipientGroupTo
    {
        public int RecipientId { get; set; }
        public Recipient Recipient { get; set; }
        public int DistributionInformationId { get; set; }
        public DistributionInformation RecipientGroup { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class RecipientGroupCc
    {
        public int RecipientId { get; set; }
        public Recipient Recipient { get; set;}
        public int RecipientGroupId { get; set; }
        public RecipientGroup RecipientGroup { get; set; }
    }
}

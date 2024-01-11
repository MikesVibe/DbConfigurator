using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class EmailData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requester { get; set; }
        public string TicketType{ get; set; }
        public string Priority { get; set; }
        public string GBU { get; set; }
        [JsonIgnore]
        public DateTime ReportedDate { get; set; }
    }
}

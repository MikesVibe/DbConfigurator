using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class NotificationData
    {
        public string TicketType { get; set; }
        public string TicketNumber { get; set; }
        public string TicketSummary { get; set; }
        public string TicketDescription { get; set; }
        public string OpenedBy { get; set; }
        public string ReportedBy { get; set; }
        public string Priority { get; set; }
        public string GBU { get; set; }
        public DateTime OpenedDate { get; set; }
        public DateTime ReportedDate { get; set; }


    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model
{
    public class Notification
    {
        public Notification()
        {

        }

        public int Id { get; set; }

        [Required]
        public int BusinessUnitId { get; set; }

        [Required]
        public int PriorityId { get; set; }

        [Required]
        public int RecipientsGroupId{ get; set; }

    }
}

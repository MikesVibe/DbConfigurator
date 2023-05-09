using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Models
{
    internal class PriorityWithIdDto
    {
        public PriorityWithIdDto(int id, string priorityName)
        {
            Id = id;
            Name = priorityName;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}

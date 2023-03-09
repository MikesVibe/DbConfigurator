using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.DTOs
{
    public class RecipientDto
    {
        public int Id { get; }
        public string Name { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.DTOs
{
    public class AreaDto
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
    }
}

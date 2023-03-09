using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.DTOs
{
    public class CountryDto
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
    }
}

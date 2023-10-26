using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.DTOs.CountryDtos
{
    public class CreateCountryDto
    {
        public string CountryName { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
    }
}

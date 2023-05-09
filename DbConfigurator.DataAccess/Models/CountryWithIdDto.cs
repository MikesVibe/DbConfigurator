using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Models
{
    internal class CountryWithIdDto
    {
        public CountryWithIdDto(int id, string name, string countryCode)
        {
            Id = id;
            Name = name;
            ShortCode = countryCode;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
    }
}

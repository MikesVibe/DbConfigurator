using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.DTOs.Core
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string CountryName { get; set; } = string.Empty;
        [MaxLength(3)]
        public string CountryCode { get; set; } = string.Empty;
    }
}

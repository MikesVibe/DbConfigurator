using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.DataAccess.DTOs.CountryDtos
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string CountryName { get; set; } = string.Empty;
        [MaxLength(3)]
        public string CountryCode { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.DataAccess.DTOs.AreaDtos
{
    public class AreaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.DTOs
{
    public class AreaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}

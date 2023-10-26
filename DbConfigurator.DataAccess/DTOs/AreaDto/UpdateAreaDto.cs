using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.DataAccess.DTOs.AreaDtos
{
    public class UpdateAreaDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

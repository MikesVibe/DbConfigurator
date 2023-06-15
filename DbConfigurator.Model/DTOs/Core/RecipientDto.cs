using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.DTOs.Core
{
    public class RecipientDto
    {
        public int Id { get; init; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}

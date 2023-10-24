using DbConfigurator.Model.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.Entities.Core
{
    public class BusinessUnit : IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
using DbConfigurator.Model.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.Entities.Core
{
    public class Priority : IEntity
    {
        //[Required]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(6)]
        public string Name { get; set; }
    }
}

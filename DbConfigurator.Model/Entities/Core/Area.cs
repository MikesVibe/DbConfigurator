﻿using DbConfigurator.Model.Contracts;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model.Entities.Core
{
    public class Area : IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        public IEntity CreateCopy()
        {
            return new Area { Id = Id, Name = Name };
        }
    }
}

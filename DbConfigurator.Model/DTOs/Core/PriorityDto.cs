﻿using DbConfigurator.Model.Contracts;

namespace DbConfigurator.Model.DTOs.Core
{
    public class PriorityDto : IEntityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

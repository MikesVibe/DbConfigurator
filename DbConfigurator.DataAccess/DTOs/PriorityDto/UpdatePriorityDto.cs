﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.DTOs.PriorityDtos
{
    public class UpdatePriorityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

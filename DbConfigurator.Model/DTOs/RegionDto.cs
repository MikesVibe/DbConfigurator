﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.DTOs
{
    public class RegionDto
    {
        public int Id { get; init; }
        public AreaDto Area { get; set; }
        public BuisnessUnitDto BuisnessUnit { get; set; }
        public CountryDto Country { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.DTOs.CountryDtos
{
    public class UpdateCountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

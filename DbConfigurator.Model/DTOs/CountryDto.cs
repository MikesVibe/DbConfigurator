﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.DTOs
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryShortCode { get; set; }
    }
}

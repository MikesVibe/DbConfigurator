﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class DistributionInfoLookup
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string BuisnessUnit { get; set; }
        public string Country { get; set; }
        public string Priority { get; set; }
        public string TO { get; set; }
        public string CC { get; set; }
    }
}

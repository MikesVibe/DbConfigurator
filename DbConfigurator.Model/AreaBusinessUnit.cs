﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class AreaBuisnessUnit
    {
        public int Id { get ; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public int BuisnessUnitId { get; set; }
        public BuisnessUnit BuisnessUnit { get; set; }
    }
}

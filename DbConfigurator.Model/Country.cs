﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class Country
    {
        public Country()
        {
            DistributionInformations = new Collection<DistributionInformation>();
        }
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        public string ShortCode{ get; set; }

        public int BuisnessUnitId { get; set; }
        public BuisnessUnit BuisnessUnit { get; set; }

        public ICollection<DistributionInformation> DistributionInformations { get; set; }

    }
}

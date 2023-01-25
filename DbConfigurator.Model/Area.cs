using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model
{
    public class Area
    {
        public Area() 
        {
            BuisnessUnits = new Collection<BuisnessUnit>();
        }


        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<BuisnessUnit> BuisnessUnits { get; set; }
    }
}

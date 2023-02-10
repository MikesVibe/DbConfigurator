using DbConfigurator.Model;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DbConfigurator.Model
{
    public class BuisnessUnit
    {
        public BuisnessUnit() 
        {
            Areas = new ObservableCollection<Area>();
            Countries = new ObservableCollection<Country>();
        }

        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

   
    
        public ICollection<Area> Areas { get; set; }
        public ICollection<Country> Countries { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.Entities.Table
{
    public class BuisnessUnitTableItem : ITableItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

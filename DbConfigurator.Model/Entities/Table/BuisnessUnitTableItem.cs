using System;
using System.Collections.Generic;
using System.Linq;
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

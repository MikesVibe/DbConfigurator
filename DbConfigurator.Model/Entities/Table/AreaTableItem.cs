using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.Model.Entities.Table
{
    public class AreaTableItem : ITableItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

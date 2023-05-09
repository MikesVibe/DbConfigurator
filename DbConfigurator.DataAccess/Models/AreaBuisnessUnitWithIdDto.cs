using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Models
{
    internal class AreaBuisnessUnitWithIdDto
    {
        public AreaBuisnessUnitWithIdDto(int id, int areaId, int buisnessUnitId)
        {
            Id = id;
            AreaId = areaId;
            BuisnessUnitId = buisnessUnitId;
        }

        public int Id { get; set; }
        public int AreaId { get; set; }
        public int BuisnessUnitId { get; set; }
    }
}

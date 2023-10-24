using DbConfigurator.DataAccess.DTOs.AreaDto;
using DbConfigurator.Model.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Controllers
{
    public class AreaController : BaseController<CreateAreaDto, UpdateAreaDto, AreaDto>
    {
        public override async Task<IEnumerable<AreaDto>> GetAll()
        {
            var dto = await _httpClient.GetFromJsonAsync<IEnumerable<AreaDto>>($"area/all");

            return new List<AreaDto>();
        }
    }
}

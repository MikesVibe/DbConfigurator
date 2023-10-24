﻿using DbConfigurator.DataAccess.DTOs.AreaDto;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;

namespace DbConfigurator.UI.Services
{
    public interface IRecipientService : IDataService<CreateAreaDto, UpdateAreaDto, Area>
    {
    }
}

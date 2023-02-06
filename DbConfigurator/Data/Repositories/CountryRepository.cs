﻿using DbConfigurator.DataAccess;
using DbConfigurator.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Data.Repositories
{
    public class CountryRepository : GenericRepository<Country, DbConfiguratorDbContext>, ICountryRepository
    {
        public CountryRepository(DbConfiguratorDbContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Country>> GetAllAsync()
        {
            var collection = await Context.Set<Country>().Include(c => c.BuisnessUnit.Area).ToListAsync();

            return collection;
        }
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            var collection = await Context.Set<Country>().ToListAsync();

            return collection;
        }

        public async Task<IEnumerable<BuisnessUnit>> GetAllBuisnessUnitsAsync()
        {
            var collection = await Context.Set<BuisnessUnit>().ToListAsync();

            return collection;
        }

        public async Task<IEnumerable<Area>> GetAllAreasAsync()
        {
            var collection = await Context.Set<Area>().ToListAsync();

            return collection;
        }


    }
}

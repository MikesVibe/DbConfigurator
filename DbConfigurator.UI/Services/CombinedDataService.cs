﻿using DbConfigurator.DataAccess;
using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using DbConfigurator.UI.Services.Interfaces;
using DbConfigurator.UI.Startup;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.UI.Services
{
    public class CombinedDataService : ICombinedDataService
    {
        private AutoMapperConfig _autoMapper;
        private DbConfiguratorDbContext _context;


        public CombinedDataService(
            DbConfiguratorDbContext dbConfiguratorDbContext,
            AutoMapperConfig autoMapperConfig,
            IRegionService regionService
            )
        {
            _context = dbConfiguratorDbContext;
            _autoMapper = autoMapperConfig;
            RegionService = regionService;
        }

        public IRegionService RegionService { get; }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<ICollection<Area>> GetAllAreasAsync()
        {
            var collection = await _context.Set<Area>().AsNoTracking().ToListAsync();

            return collection;
        }
        public async Task<ICollection<BuisnessUnit>> GetAllBuisnessUnitsAsync()
        {
            var collection = await _context.Set<BuisnessUnit>().AsNoTracking().ToListAsync();

            return collection;
        }
        public async Task<ICollection<Recipient>> GetAllRecipientsAsync()
        {
            var collection = await _context.Set<Recipient>().AsNoTracking().ToListAsync();
            return collection;
        }
        public async Task<ICollection<Country>> GetAllCountriesAsync()
        {
            var collection = await _context.Set<Country>().ToListAsync();
            //.Include(c => c.BuisnessUnits).ThenInclude(bu => bu.Areas).AsNoTracking().ToListAsync();

            return collection;
        }

        public async Task AddAsync<T>(T item) where T : class
        {
            await _context.Set<T>().AddAsync(item);
        }
        public void Add<T>(T item) where T : class
        {
            _context.Set<T>().Add(item);
        }
        public void Remove<T>(T item) where T : class
        {
            _context.Set<T>().Remove(item);
        }
        public async Task<Recipient> GetRecipientByIdAsync(int id)
        {
            return await _context.Recipient.Where(r => r.Id == id).FirstAsync();
        }
        public Region? GetRegionById(int id)
        {
            return GetRegionsAsQueryable().Where(r => r.Id == id).AsNoTracking().FirstOrDefault();
        }
        public Area? GetAreaById(int id)
        {
            return _context.Area.Where(a => a.Id == id).FirstOrDefault();
        }
        public BuisnessUnit? GetBuisnessUnitById(int id)
        {
            return _context.BuisnessUnit.Where(a => a.Id == id).FirstOrDefault();
        }
        public Country? GetCountryById(int id)
        {
            return _context.Country.Where(a => a.Id == id).FirstOrDefault();
        }
        private IQueryable<Region> GetRegionsAsQueryable()
        {
            return _context.Set<Region>()
                .Include(r => r.Area)
                .Include(r => r.BuisnessUnit)
                .Include(r => r.Country)
                .AsQueryable();
        }
    }
}
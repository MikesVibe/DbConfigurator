using DbConfigurator.Model.DTOs.Core;
using DbConfigurator.Model.Entities.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbConfigurator.DataAccess.Repository
{
    public class DistributionInformationRepository : GenericRepository<DistributionInformation>
    {
        public DistributionInformationRepository(DbConfiguratorDbContext dbContext) : base(dbContext)
        {
        }

        //public override async Task UpdateAsync(DistributionInformation entity)
        //{
        //    var existing = await _context.Set<DistributionInformation>().FindAsync(entity.Id);
        //    if (existing is null)
        //        return;
        //    _context.Entry(entity).State = EntityState.Detached;


        //    // Update scalar and complex properties
        //    _context.Entry(existing).CurrentValues.SetValues(entity);

        //    // Clear the existing collection
        //    existing.RecipientsTo.Clear();
        //    foreach (var recipientTo in entity.RecipientsTo)
        //    {
        //        // Add each new Recipient
        //        existing.RecipientsTo.Add(recipientTo);
        //    }

        //    existing.RecipientsCc.Clear();
        //    foreach (var recipientCc in entity.RecipientsCc)
        //    {
        //        // Add each new Recipient
        //        existing.RecipientsCc.Add(recipientCc);
        //    }
        //}

        public override async Task<IEnumerable<DistributionInformation>> GetAllAsync()
        {
            return await _context.Set<DistributionInformation>()
                .Include(d => d.Region).ThenInclude(r => r.Area)
                .Include(d => d.Region).ThenInclude(r => r.BuisnessUnit)
                .Include(d => d.Region).ThenInclude(r => r.Country)
                .Include(d => d.RecipientsTo)
                .Include(d => d.RecipientsCc)
                .Include(d => d.Priority)
                .AsNoTracking().ToListAsync();
        }

        public override async Task<DistributionInformation?> GetByIdAsync(int id)
        {
            return await _context.DistributionInformation.Where(d => d.Id == id)
             .Include(d => d.Region).ThenInclude(r => r.Area)
             .Include(d => d.Region).ThenInclude(r => r.BuisnessUnit)
             .Include(d => d.Region).ThenInclude(r => r.Country)
             .Include(r => r.Region)
             .Include(t => t.RecipientsTo)
             .Include(t => t.RecipientsCc)
             .Include(p => p.Priority)
             .FirstOrDefaultAsync();
        }

        public async Task<List<Area>> GetAreasAsync()
        {
            var regions = GetRegionsAsQueryable();
            var AreasIdList = await regions.Select(r => r.AreaId).Distinct().ToListAsync();
            var areas = await _context.Area.Where(a => AreasIdList.Contains(a.Id)).ToListAsync();

            return areas;
        }
        public async Task<List<BuisnessUnit>> GetBuisnessUnitsAsync(int? areaId = null)
        {
            var regions = areaId is null ?
                GetRegionsAsQueryable() :
                GetRegionsAsQueryable().Where(r => r.AreaId == areaId);

            var buisnessUnitsIdList = await regions.Select(r => r.BuisnessUnitId).ToListAsync();

            var buisnessUnits = await GetBuisnessUnitsAsQueryable().Where(b => buisnessUnitsIdList.Contains(b.Id)).ToListAsync();


            return buisnessUnits;
        }
        public async Task<List<Country>> GetCountriesAsync(int? buisnessUnitId = null)
        {
            var regions = buisnessUnitId is null ?
                GetRegionsAsQueryable() :
                GetRegionsAsQueryable().Where(r => r.BuisnessUnitId == buisnessUnitId);

            var countriesIdList = await regions.Select(r => r.CountryId).ToListAsync();

            var countries = await GetCountriesAsQueryable().Where(c => countriesIdList.Contains(c.Id)).ToListAsync();


            return countries;
        }

        public async Task AddRecipientCcAsync(int distributionInformationId, Recipient recipientEntity)
        {
            var distributionInformation = await _context.Set<DistributionInformation>().FindAsync(distributionInformationId);
            if (distributionInformation is null)
                throw new ArgumentNullException(nameof(distributionInformation));

            distributionInformation.RecipientsCc.Add(recipientEntity);
        }

        public async Task AddRecipientToAsync(int distributionInformationId, Recipient recipientEntity)
        {
            var distributionInformation = await _context.Set<DistributionInformation>().FindAsync(distributionInformationId);
            if (distributionInformation is null)
                throw new ArgumentNullException(nameof(distributionInformation));

            distributionInformation.RecipientsTo.Add(recipientEntity);
        }
    }
}

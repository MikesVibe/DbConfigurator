﻿using DbConfigurator.Model.DTOs.Core;
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

        public override async Task UpdateAsync(DistributionInformation entity)
        {
            var existing = await _context.Set<DistributionInformation>().FindAsync(entity.Id);
            if (existing is null)
                return;

            //Detaching recipients 
            foreach (var recipientTo in existing.RecipientsTo)
            {
                _context.Entry(recipientTo).State = EntityState.Detached;
            }

            foreach (var recipientCc in existing.RecipientsCc)
            {
                _context.Entry(recipientCc).State = EntityState.Detached;
            }

            // Update scalar and complex properties
            _context.Entry(existing).CurrentValues.SetValues(entity);


        }

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

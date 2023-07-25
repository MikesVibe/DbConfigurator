using DbConfigurator.Model.Entities.Core;
using Microsoft.EntityFrameworkCore;
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

        public override async Task UpdateAsync(DistributionInformation distributionInformation)
        {
            var entity = await GetByIdAsync(distributionInformation.Id);
            if (entity is null)
            {
                return;
            }

            entity.RegionId = distributionInformation.RegionId;
            entity.PriorityId = distributionInformation.PriorityId;

            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;


            // Optimized Handling the RecipientsTo
            UpdateRecipients(entity.RecipientsTo, distributionInformation.RecipientsTo);

            // Optimized Handling the RecipientsCc
            UpdateRecipients(entity.RecipientsCc, distributionInformation.RecipientsCc);

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
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
                .AsNoTracking()
                .ToListAsync();
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
             .AsNoTracking()
             .FirstOrDefaultAsync();
        }

        public async Task AddRecipientCcAsync(int distributionInformationId, int recipientId)
        {
            var distributionInformation = await _context.Set<DistributionInformation>().FindAsync(distributionInformationId);
            var recipientEntity = await _context.Set<Recipient>().FindAsync(recipientId);

            if (distributionInformation is null || recipientEntity is null)
                return;

            distributionInformation.RecipientsCc.Add(recipientEntity);
        }

        public async Task AddRecipientToAsync(int distributionInformationId, int recipientId)
        {
            var distributionInformation = await _context.Set<DistributionInformation>().FindAsync(distributionInformationId);
            var recipientEntity = await _context.Set<Recipient>().FindAsync(recipientId);
            if (distributionInformation is null || recipientEntity is null)
                return;

            distributionInformation.RecipientsTo.Add(recipientEntity);
        }

        private void UpdateRecipients(ICollection<Recipient> existingRecipients, ICollection<Recipient> newRecipientsDto)
        {
            var recipientsToRemove = existingRecipients.Where(e => !newRecipientsDto.Any(n => n.Id == e.Id)).ToList();
            foreach (var recipient in recipientsToRemove)
            {
                existingRecipients.Remove(recipient);
            }

            var recipientsToAdd = newRecipientsDto.Where(n => !existingRecipients.Any(e => e.Id == n.Id)).ToList();
            foreach (var recipientDto in recipientsToAdd)
            {
                var entity = _context.Recipient.Where(r => r.Id == recipientDto.Id).First();
                existingRecipients.Add(entity);
            }
        }

        public override async Task<int> AddAsync(DistributionInformation disInfo)
        {
            var entity = new DistributionInformation
            {
                RegionId = disInfo.RegionId,
                PriorityId = disInfo.PriorityId
            };
            _context.DistributionInformation.Add(entity);

            // Optimized Handling the RecipientsTo
            UpdateRecipients(entity.RecipientsTo, disInfo.RecipientsTo);

            // Optimized Handling the RecipientsCc
            UpdateRecipients(entity.RecipientsCc, disInfo.RecipientsCc);
            await SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Detached;
            return entity.Id;
        }
    }
}

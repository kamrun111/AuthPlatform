using AuthPlatform.Domain.Expenditures.Entities;
using AuthPlatform.Domain.Expenditures.Interfaces;
using AuthPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthPlatform.Infrastructure.Expenditures.EF
{

    public class ExpenditureInvoiceDetailRepository : IExpenditureInvoiceDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenditureInvoiceDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpenditureInvoiceDetail>> GetByInvoiceIdAsync(
            int expenditureInvoiceId)
        {
            return await _context.ExpenditureInvoiceDetails
                .Where(x =>
                    x.ExpenditureInvoiceId == expenditureInvoiceId &&
                    x.IsActive)
                .ToListAsync();
        }

        public async Task<ExpenditureInvoiceDetail?> GetByIdAsync(
            int expenditureInvoiceDetailId)
        {
            return await _context.ExpenditureInvoiceDetails
                .FirstOrDefaultAsync(x =>
                    x.ExpenditureInvoiceDetailId == expenditureInvoiceDetailId);
        }

        public async Task AddAsync(ExpenditureInvoiceDetail entity)
        {
            await _context.ExpenditureInvoiceDetails.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ExpenditureInvoiceDetail entity)
        {
            _context.ExpenditureInvoiceDetails.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ExpenditureInvoiceDetail entity)
        {
            entity.IsActive = false;

            entity.RecordUpdatedDate = DateTime.UtcNow;

            _context.ExpenditureInvoiceDetails.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}

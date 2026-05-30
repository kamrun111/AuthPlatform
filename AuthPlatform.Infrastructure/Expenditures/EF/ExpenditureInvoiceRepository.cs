using AuthPlatform.Domain.Expenditures.Entities;
using AuthPlatform.Domain.Expenditures.Interfaces;
using AuthPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;



namespace AuthPlatform.Infrastructure.Expenditures.EF
{



    public class ExpenditureInvoiceRepository : IExpenditureInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenditureInvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpenditureInvoice>> GetAllAsync()
        {
            return await _context.ExpenditureInvoices
                .Include(x => x.ExpenditureHead)
                .Include(x => x.Details)
                .OrderByDescending(x => x.IsActive)
                .ThenByDescending(x => x.InvoiceDate)
                .ToListAsync();
        }

        public async Task<ExpenditureInvoice?> GetByIdAsync(int expenditureInvoiceId)
        {
            return await _context.ExpenditureInvoices
                .Include(x => x.ExpenditureHead)
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x =>
                    x.ExpenditureInvoiceId == expenditureInvoiceId);
        }

        public async Task AddAsync(ExpenditureInvoice entity)
        {
            await _context.ExpenditureInvoices.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ExpenditureInvoice entity)
        {
            _context.ExpenditureInvoices.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ExpenditureInvoice entity)
        {
            entity.IsActive = false;

            entity.RecordUpdatedDate = DateTime.UtcNow;

            foreach (var detail in entity.Details)
            {
                detail.IsActive = false;

                detail.RecordUpdatedDate = DateTime.UtcNow;
            }

            _context.ExpenditureInvoices.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetInvoiceCountByMonthAsync(int year, int month)
        {
            return await _context.ExpenditureInvoices
                .CountAsync(x =>
                    x.InvoiceDate.Year == year &&
                    x.InvoiceDate.Month == month);
        }
    }
}

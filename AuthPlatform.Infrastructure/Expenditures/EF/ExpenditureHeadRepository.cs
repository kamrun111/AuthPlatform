using AuthPlatform.Domain.Expenditures.Entities;
using AuthPlatform.Domain.Expenditures.Interfaces;
using AuthPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthPlatform.Infrastructure.Expenditures.EF
{




    public class ExpenditureHeadRepository : IExpenditureHeadRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenditureHeadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpenditureHead>> GetAllAsync()
        {
            return await _context.ExpenditureHeads
                       .OrderByDescending(x => x.IsActive)
                       .ThenBy(x => x.ExpenditureHeadName)
                       .ToListAsync();
        }

        public async Task<ExpenditureHead?> GetByIdAsync(int expenditureHeadId)
        {
            return await _context.ExpenditureHeads
                        .FirstOrDefaultAsync(x =>
                            x.ExpenditureHeadId == expenditureHeadId);
        }

        public async Task AddAsync(ExpenditureHead entity)
        {
            await _context.ExpenditureHeads.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ExpenditureHead entity)
        {
            _context.ExpenditureHeads.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ExpenditureHead entity)
        {
            entity.IsActive = false;
            entity.RecordUpdatedDate = DateTime.UtcNow;

            _context.ExpenditureHeads.Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}

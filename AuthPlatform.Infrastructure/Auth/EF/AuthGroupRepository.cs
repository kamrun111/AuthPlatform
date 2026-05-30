using AuthPlatform.Domain.Auth.Entities;
using AuthPlatform.Domain.Auth.Interfaces;
using AuthPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthPlatform.Infrastructure.Auth.EF
{
  

    public class AuthGroupRepository : IAuthGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthGroup>> GetAllAsync()
        {
            return await _context.AuthGroups.ToListAsync();
        }

        public async Task<AuthGroup?> GetByIdAsync(int authGroupId)
        {
            return await _context.AuthGroups
                .Include(x => x.AuthGroupPermissions)
                .FirstOrDefaultAsync(x => x.AuthGroupId == authGroupId);
        }

        public async Task<AuthGroup?> GetByNameAsync(string roleName)
        {
            return await _context.AuthGroups
                .FirstOrDefaultAsync(x => x.GroupName == roleName);
        }

        public async Task AddAsync(AuthGroup entity)
        {
            await _context.AuthGroups.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AuthGroup entity)
        {
            _context.AuthGroups.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AuthGroup entity)
        {
            _context.AuthGroups.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}

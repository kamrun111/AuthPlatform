using AuthPlatform.Domain.Auth.Entities;
using AuthPlatform.Domain.Auth.Interfaces;
using AuthPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthPlatform.Infrastructure.Auth.EF
{
    public class AuthUserRepository : IAuthUserRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AuthUser?> GetByIdAsync(int authUserId)
        {
            return await _context.AuthUsers
                .Include(x => x.UserPermissions)
                .Include(x => x.AuthUserGroups)
                .FirstOrDefaultAsync(x => x.AuthUserId == authUserId);
        }

        public async Task<AuthUser?> GetByUserNameAsync(string userName)
        {
            return await _context.AuthUsers
                .Include(x => x.UserPermissions)
                .Include(x => x.AuthUserGroups)
                .FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<AuthUser?> GetByEmailAsync(string email)
        {
            return await _context.AuthUsers
                .Include(x => x.UserPermissions)
                .Include(x => x.AuthUserGroups)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<AuthUser>> GetAllAsync()
        {
            return await _context.AuthUsers
                .Include(x => x.UserPermissions)
                .Include(x => x.AuthUserGroups)
                .ToListAsync();
        }

        public async Task AddAsync(AuthUser entity)
        {
            await _context.AuthUsers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AuthUser entity)
        {
            _context.AuthUsers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AuthUser entity)
        {
            _context.AuthUsers.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
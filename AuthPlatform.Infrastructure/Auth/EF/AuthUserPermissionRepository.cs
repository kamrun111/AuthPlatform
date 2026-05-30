using AuthPlatform.Domain.Auth.Entities;
using AuthPlatform.Domain.Auth.Interfaces;
using AuthPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthPlatform.Infrastructure.Auth.EF
{
   

   

    public class AuthUserPermissionRepository : IAuthUserPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthUserPermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthUserPermission>> GetByUserIdAsync(int authUserId)
        {
            return await _context.AuthUserPermissions
                .Include(x => x.AuthPermission)
                .Where(x => x.AuthUserId == authUserId)
                .ToListAsync();
        }

        public async Task AddAsync(AuthUserPermission entity)
        {
            await _context.AuthUserPermissions.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AuthUserPermission entity)
        {
            _context.AuthUserPermissions.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}

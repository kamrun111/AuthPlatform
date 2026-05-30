using AuthPlatform.Domain.Auth.Entities;
using AuthPlatform.Domain.Auth.Interfaces;
using AuthPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;



namespace AuthPlatform.Infrastructure.Auth.EF
{
   

    public class AuthGroupPermissionRepository : IAuthGroupPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthGroupPermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthGroupPermission>> GetByGroupIdAsync(int authGroupId)
        {
            return await _context.AuthGroupPermissions
                .Include(x => x.AuthPermission)
                .Where(x => x.AuthGroupId == authGroupId)
                .ToListAsync();
        }

        public async Task AddAsync(AuthGroupPermission entity)
        {
            await _context.AuthGroupPermissions.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AuthGroupPermission entity)
        {
            _context.AuthGroupPermissions.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}

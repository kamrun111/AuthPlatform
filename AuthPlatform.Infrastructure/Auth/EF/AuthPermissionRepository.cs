using AuthPlatform.Domain.Auth.Entities;
using AuthPlatform.Domain.Auth.Interfaces;
using AuthPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuthPlatform.Infrastructure.Auth.EF
{
    public class AuthPermissionRepository : IAuthPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthPermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthPermission>> GetAllAsync()
        {
            return await _context.AuthPermissions.ToListAsync();
        }

        public async Task<AuthPermission?> GetByIdAsync(int authPermissionId)
        {
            return await _context.AuthPermissions
                .FirstOrDefaultAsync(x => x.AuthPermissionId == authPermissionId);
        }

        public async Task<AuthPermission?> GetByCodeAsync(string permissionCode)
        {
            return await _context.AuthPermissions
                .FirstOrDefaultAsync(x => x.PermissionCode == permissionCode);
        }

        public async Task<List<string>> GetUserEffectivePermissionCodesAsync(int authUserId)
        {
            var directPermissions = await _context.AuthUserPermissions
                .Where(x => x.AuthUserId == authUserId)
                .Select(x => x.AuthPermission.PermissionCode)
                .ToListAsync();

            var rolePermissions = await _context.AuthUserGroups
                .Where(x => x.AuthUserId == authUserId)
                .SelectMany(x => x.AuthGroup.AuthGroupPermissions)
                .Select(x => x.AuthPermission.PermissionCode)
                .ToListAsync();

            return directPermissions
                .Union(rolePermissions)
                .Distinct()
                .ToList();
        }

        public async Task AddAsync(AuthPermission entity)
        {
            await _context.AuthPermissions.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AuthPermission entity)
        {
            _context.AuthPermissions.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AuthPermission entity)
        {
            _context.AuthPermissions.Remove(entity);

            await _context.SaveChangesAsync();
        }

    }
}
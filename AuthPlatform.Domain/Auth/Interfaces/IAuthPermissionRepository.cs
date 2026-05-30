using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Auth.Entities;

namespace AuthPlatform.Domain.Auth.Interfaces
{
    public interface IAuthPermissionRepository
    {
        Task<AuthPermission?> GetByIdAsync(int id);
        Task<AuthPermission?> GetByCodeAsync(string code);
        Task<List<AuthPermission>> GetAllAsync();
        Task<List<string>> GetUserEffectivePermissionCodesAsync(int userId);
        Task AddAsync(AuthPermission permission);
        Task UpdateAsync(AuthPermission permission);
        Task DeleteAsync(AuthPermission entity);
    }
}

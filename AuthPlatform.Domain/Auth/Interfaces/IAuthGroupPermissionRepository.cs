using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Auth.Entities;

namespace AuthPlatform.Domain.Auth.Interfaces
{
    public interface IAuthGroupPermissionRepository
    {
        Task<List<AuthGroupPermission>> GetByGroupIdAsync(int authRoleId);

        Task AddAsync(AuthGroupPermission entity);

        Task DeleteAsync(AuthGroupPermission entity);
    }
}

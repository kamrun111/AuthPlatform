using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Auth.Entities;

namespace AuthPlatform.Domain.Auth.Interfaces
{
    public interface IAuthUserPermissionRepository
    {
        Task<List<AuthUserPermission>> GetByUserIdAsync(int authUserId);

        Task AddAsync(AuthUserPermission entity);

        Task DeleteAsync(AuthUserPermission entity);
    }

}

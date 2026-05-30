using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Auth.Entities;

namespace AuthPlatform.Domain.Auth.Interfaces
{
    public interface IAuthUserGroupRepository
    {

        Task AddAsync(AuthUserGroup entity);

        Task DeleteAsync(AuthUserGroup entity);
        Task<List<AuthUserGroup>> GetByUserIdAsync(int authUserId);

        
    }
}

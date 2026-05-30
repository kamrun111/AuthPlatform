using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Auth.Entities;

namespace AuthPlatform.Domain.Auth.Interfaces
{
    public interface IAuthGroupRepository
    {
        Task<List<AuthGroup>> GetAllAsync();

        Task<AuthGroup?> GetByIdAsync(int authRoleId);

        Task<AuthGroup?> GetByNameAsync(string roleName);

        Task AddAsync(AuthGroup entity);

        Task UpdateAsync(AuthGroup entity);

        Task DeleteAsync(AuthGroup entity);
    }
}

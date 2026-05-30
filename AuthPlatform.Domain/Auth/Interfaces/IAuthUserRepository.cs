using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Auth.Entities;

namespace AuthPlatform.Domain.Auth.Interfaces
{
    public interface IAuthUserRepository
    {
        Task<AuthUser?> GetByIdAsync(int id);
        Task<AuthUser?> GetByUserNameAsync(string userName);
        Task<AuthUser?> GetByEmailAsync(string email);
        Task AddAsync(AuthUser user);
        Task UpdateAsync(AuthUser user);
        Task DeleteAsync(AuthUser user);
        Task<List<AuthUser>> GetAllAsync();


    }
}

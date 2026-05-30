using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Auth.Entities;

namespace AuthPlatform.Domain.Auth.Interfaces
{
    public interface IAuthRefreshTokenRepository
    {
        Task<AuthRefreshToken?> GetByTokenAsync(string token);
        Task AddAsync(AuthRefreshToken refreshToken);
        Task UpdateAsync(AuthRefreshToken refreshToken);
        Task RevokeAsync(string token);
    }
}

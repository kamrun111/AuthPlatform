using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Application.Common.Responses;

namespace AuthPlatform.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);

        Task<ApiResponse<string>> RegisterAsync(RegisterUserDto request);

        Task<ApiResponse<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);

        Task<ApiResponse<string>> LogoutAsync(string refreshToken);
    }
}

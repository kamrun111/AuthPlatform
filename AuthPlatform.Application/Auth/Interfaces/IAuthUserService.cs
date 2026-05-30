
using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Application.Common.Responses;

namespace AuthPlatform.Application.Auth.Interfaces
{
    public interface IAuthUserService
    {
        Task<ApiResponse<List<AuthUserListDto>>> GetAllAsync();

        Task<ApiResponse<AuthUserListDto>> GetByIdAsync(int authUserId);

        Task<ApiResponse<string>> CreateAsync(CreateUserDto request);

        Task<ApiResponse<string>> UpdateAsync(CreateUserDto request);
    }
}

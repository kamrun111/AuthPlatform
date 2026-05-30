using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Application.Common.Responses;

namespace AuthPlatform.Application.Auth.Interfaces
{
    public interface IAuthPermissionService
    {
        Task<ApiResponse<List<AuthPermissionDto>>> GetAllAsync();

        Task<ApiResponse<AuthPermissionDto>> GetByIdAsync(int authPermissionId);

        Task<ApiResponse<string>> CreateAsync(AuthPermissionDto request);

        Task<ApiResponse<string>> UpdateAsync(AuthPermissionDto request);

        Task<ApiResponse<string>> DeleteAsync(int authPermissionId);

        Task<ApiResponse<string>> AssignPermissionToUserAsync(AuthUserPermissionDto request);

        Task<ApiResponse<string>> AssignPermissionToGroupAsync(AuthGroupPermissionDto request);

        Task<ApiResponse<List<string>>> GetUserPermissionCodesAsync(int authUserId);

        Task<ApiResponse<List<string>>> GetGroupPermissionCodesAsync(int authGroupId);
        Task<ApiResponse<string>> SaveGroupPermissionsAsync(
    SaveGroupPermissionsDto request);
        Task<ApiResponse<string>> SaveUserPermissionsAsync(
    SaveUserPermissionsDto request);
    }
}
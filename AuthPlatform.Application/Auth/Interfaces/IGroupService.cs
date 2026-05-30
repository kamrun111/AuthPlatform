using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Application.Common.Responses;

namespace AuthPlatform.Application.Auth.Interfaces
{
    public interface IGroupService
    {
        Task<ApiResponse<List<AuthGroupDto>>> GetAllAsync();

        Task<ApiResponse<AuthGroupDto>> GetByIdAsync(int authRoleId);

        Task<ApiResponse<string>> CreateAsync(AuthGroupDto request);

        Task<ApiResponse<string>> UpdateAsync(AuthGroupDto request);

        Task<ApiResponse<string>> DeleteAsync(int authRoleId);
    }
}

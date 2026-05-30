using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Application.Auth.Interfaces;
using AuthPlatform.Application.Common.Responses;
using AuthPlatform.Domain.Auth.Entities;
using AuthPlatform.Domain.Auth.Interfaces;
using AutoMapper;

namespace AuthPlatform.Application.Auth.Services
{
    public class PermissionService : IAuthPermissionService
    {
        private readonly IAuthPermissionRepository _permissionRepository;
        private readonly IAuthUserPermissionRepository _userPermissionRepository;
        private readonly IAuthGroupPermissionRepository _groupPermissionRepository;
        private readonly IMapper _mapper;

        public PermissionService(
            IAuthPermissionRepository permissionRepository,
            IAuthUserPermissionRepository userPermissionRepository,
            IAuthGroupPermissionRepository groupPermissionRepository,
            IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _userPermissionRepository = userPermissionRepository;
            _groupPermissionRepository = groupPermissionRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<AuthPermissionDto>>> GetAllAsync()
        {
            var entities = await _permissionRepository.GetAllAsync();

            return new ApiResponse<List<AuthPermissionDto>>
            {
                Success = true,
                Message = "Permissions loaded successfully.",
                Data = _mapper.Map<List<AuthPermissionDto>>(entities)
            };
        }

        public async Task<ApiResponse<AuthPermissionDto>> GetByIdAsync(int authPermissionId)
        {
            var entity = await _permissionRepository.GetByIdAsync(authPermissionId);

            if (entity == null)
            {
                return new ApiResponse<AuthPermissionDto>
                {
                    Success = false,
                    Message = "Permission not found."
                };
            }

            return new ApiResponse<AuthPermissionDto>
            {
                Success = true,
                Message = "Permission loaded successfully.",
                Data = _mapper.Map<AuthPermissionDto>(entity)
            };
        }

        public async Task<ApiResponse<string>> CreateAsync(AuthPermissionDto request)
        {
            var entity = _mapper.Map<AuthPermission>(request);

            await _permissionRepository.AddAsync(entity);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Permission created successfully."
            };
        }

        public async Task<ApiResponse<string>> UpdateAsync(AuthPermissionDto request)
        {
            var entity = await _permissionRepository.GetByIdAsync(request.AuthPermissionId);

            if (entity == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Permission not found."
                };
            }

            entity.PermissionName = request.PermissionName;
            entity.PermissionCode = request.PermissionCode;
            entity.Description = request.Description;
            entity.IsActive = request.IsActive;

            await _permissionRepository.UpdateAsync(entity);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Permission updated successfully."
            };
        }

        public async Task<ApiResponse<string>> DeleteAsync(int authPermissionId)
        {
            var entity = await _permissionRepository.GetByIdAsync(authPermissionId);

            if (entity == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Permission not found."
                };
            }

            await _permissionRepository.DeleteAsync(entity);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Permission deleted successfully."
            };
        }

        public async Task<ApiResponse<string>> AssignPermissionToUserAsync(AuthUserPermissionDto request)
        {
            var entity = _mapper.Map<AuthUserPermission>(request);

            await _userPermissionRepository.AddAsync(entity);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Permission assigned to user successfully."
            };
        }

        public async Task<ApiResponse<string>> AssignPermissionToGroupAsync(AuthGroupPermissionDto request)
        {
            var entity = _mapper.Map<AuthGroupPermission>(request);

            await _groupPermissionRepository.AddAsync(entity);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Permission assigned to group successfully."
            };
        }

        public async Task<ApiResponse<List<string>>> GetUserPermissionCodesAsync(int authUserId)
        {
            var permissions = await _userPermissionRepository.GetByUserIdAsync(authUserId);

            var permissionCodes = permissions
                .Where(x => x.IsActive && x.AuthPermission.IsActive)
                .Select(x => x.AuthPermission.PermissionCode)
                .Distinct()
                .ToList();

            return new ApiResponse<List<string>>
            {
                Success = true,
                Message = "User permissions loaded successfully.",
                Data = permissionCodes
            };
        }

        public async Task<ApiResponse<List<string>>> GetGroupPermissionCodesAsync(int authGroupId)
        {
            var permissions = await _groupPermissionRepository.GetByGroupIdAsync(authGroupId);

            var permissionCodes = permissions
                .Where(x => x.IsActive && x.AuthPermission.IsActive)
                .Select(x => x.AuthPermission.PermissionCode)
                .Distinct()
                .ToList();

            return new ApiResponse<List<string>>
            {
                Success = true,
                Message = "Group permissions loaded successfully.",
                Data = permissionCodes
            };
        }

        public async Task<ApiResponse<string>> SaveGroupPermissionsAsync(SaveGroupPermissionsDto request)
        {
            // Load old permissions
            var existingPermissions =
                await _groupPermissionRepository
                    .GetByGroupIdAsync(request.AuthGroupId);

            // Delete old permissions
            foreach (var item in existingPermissions)
            {
                await _groupPermissionRepository.DeleteAsync(item);
            }

            // Insert selected permissions
            foreach (var permissionId in request.AuthPermissionIds)
            {
                var entity = new AuthGroupPermission
                {
                    AuthGroupId = request.AuthGroupId,
                    AuthPermissionId = permissionId,
                    IsActive = true
                };

                await _groupPermissionRepository.AddAsync(entity);
            }

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Group permissions saved successfully."
            };
        }

        public async Task<ApiResponse<string>> SaveUserPermissionsAsync( SaveUserPermissionsDto request)
        {
            var existingPermissions =await _userPermissionRepository.GetByUserIdAsync(request.AuthUserId);

            foreach (var item in existingPermissions)
            {
                await _userPermissionRepository.DeleteAsync(item);
            }

            foreach (var permissionId in request.AuthPermissionIds)
            {
                var entity = new AuthUserPermission
                {
                    AuthUserId = request.AuthUserId,
                    AuthPermissionId = permissionId,
                    IsActive = true
                };

                await _userPermissionRepository.AddAsync(entity);
            }

            return new ApiResponse<string>
            {
                Success = true,
                Message = "User permissions saved successfully."
            };
        }
    }
}
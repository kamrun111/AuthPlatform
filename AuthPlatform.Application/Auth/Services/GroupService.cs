using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Application.Auth.Interfaces;
using AuthPlatform.Application.Common.Responses;
using AuthPlatform.Domain.Auth.Entities;
using AuthPlatform.Domain.Auth.Interfaces;
using AutoMapper;

namespace AuthPlatform.Application.Auth.Services
{
    public class GroupService : IGroupService
    {
        private readonly IAuthGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(
            IAuthGroupRepository groupRepository,
            IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<AuthGroupDto>>> GetAllAsync()
        {
            var groups = await _groupRepository.GetAllAsync();

            return new ApiResponse<List<AuthGroupDto>>
            {
                Success = true,
                Message = "Groups loaded successfully.",
                Data = _mapper.Map<List<AuthGroupDto>>(groups)
            };
        }

        public async Task<ApiResponse<AuthGroupDto>> GetByIdAsync(int authGroupId)
        {
            var group = await _groupRepository.GetByIdAsync(authGroupId);

            if (group == null)
            {
                return new ApiResponse<AuthGroupDto>
                {
                    Success = false,
                    Message = "Group not found."
                };
            }

            return new ApiResponse<AuthGroupDto>
            {
                Success = true,
                Message = "Group loaded successfully.",
                Data = _mapper.Map<AuthGroupDto>(group)
            };
        }

        public async Task<ApiResponse<string>> CreateAsync(AuthGroupDto request)
        {
            var group = _mapper.Map<AuthGroup>(request);

            await _groupRepository.AddAsync(group);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Group created successfully."
            };
        }

        public async Task<ApiResponse<string>> UpdateAsync(AuthGroupDto request)
        {
            var group = await _groupRepository.GetByIdAsync(request.AuthGroupId);

            if (group == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Group not found."
                };
            }

            group.GroupName = request.GroupName;
            group.Description = request.Description;
            group.IsActive = request.IsActive;

            await _groupRepository.UpdateAsync(group);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Group updated successfully."
            };
        }

        public async Task<ApiResponse<string>> DeleteAsync(int authGroupId)
        {
            var group = await _groupRepository.GetByIdAsync(authGroupId);

            if (group == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Group not found."
                };
            }

            await _groupRepository.DeleteAsync(group);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Group deleted successfully."
            };
        }
    }
}
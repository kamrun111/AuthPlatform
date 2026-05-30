using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Application.Auth.Interfaces;
using AuthPlatform.Application.Auth.Security;
using AuthPlatform.Application.Common.Responses;
using AuthPlatform.Domain.Auth.Entities;
using AuthPlatform.Domain.Auth.Interfaces;
using AutoMapper;

namespace AuthPlatform.Application.Auth.Services
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IAuthUserRepository _userRepository;
        private readonly IAuthUserGroupRepository _userGroupRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public AuthUserService(
            IAuthUserRepository userRepository,
            IAuthUserGroupRepository userGroupRepository,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userGroupRepository = userGroupRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<AuthUserListDto>>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return new ApiResponse<List<AuthUserListDto>>
            {
                Success = true,
                Message = "Users loaded successfully.",
                Data = _mapper.Map<List<AuthUserListDto>>(users)
            };
        }

        public async Task<ApiResponse<AuthUserListDto>> GetByIdAsync(int authUserId)
        {
            var user = await _userRepository.GetByIdAsync(authUserId);

            if (user == null)
            {
                return new ApiResponse<AuthUserListDto>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var dto = _mapper.Map<AuthUserListDto>(user);

            dto.AuthGroupIds = user.AuthUserGroups
                .Where(x => x.IsActive)
                .Select(x => x.AuthGroupId)
                .ToList();

            return new ApiResponse<AuthUserListDto>
            {
                Success = true,
                Message = "User loaded successfully.",
                Data = dto
            };
        }

        public async Task<ApiResponse<string>> CreateAsync(CreateUserDto request)
        {
            var existingUser = await _userRepository.GetByUserNameAsync(request.UserName);

            if (existingUser != null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Username already exists."
                };
            }

            var existingEmail = await _userRepository.GetByEmailAsync(request.Email);

            if (existingEmail != null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Email already exists."
                };
            }

            var user = new AuthUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                IsLocked = false,
                FailedLoginAttempts = 0,
                IsActive = true
            };

            await _userRepository.AddAsync(user);

            foreach (var groupId in request.AuthGroupIds)
            {
                var userGroup = new AuthUserGroup
                {
                    AuthUserId = user.AuthUserId,
                    AuthGroupId = groupId,
                    IsActive = true
                };

                await _userGroupRepository.AddAsync(userGroup);
            }

            return new ApiResponse<string>
            {
                Success = true,
                Message = "User created successfully."
            };
        }


        public async Task<ApiResponse<string>> UpdateAsync(CreateUserDto request)
        {
            var user = await _userRepository.GetByIdAsync(request.AuthUserId);

            if (user == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            user.Email = request.Email;
            user.IsActive = request.IsActive;

            await _userRepository.UpdateAsync(user);

            var existingGroups = await _userGroupRepository
                .GetByUserIdAsync(request.AuthUserId);

            foreach (var group in existingGroups)
            {
                await _userGroupRepository.DeleteAsync(group);
            }

            foreach (var groupId in request.AuthGroupIds)
            {
                var userGroup = new AuthUserGroup
                {
                    AuthUserId = request.AuthUserId,
                    AuthGroupId = groupId,
                    IsActive = true
                };

                await _userGroupRepository.AddAsync(userGroup);
            }

            return new ApiResponse<string>
            {
                Success = true,
                Message = "User updated successfully."
            };
        }
    }
}
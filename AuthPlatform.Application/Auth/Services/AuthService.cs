using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Application.Auth.Interfaces;
using AuthPlatform.Application.Auth.Security;
using AuthPlatform.Application.Common.Responses;
using AuthPlatform.Domain.Auth.Interfaces;

namespace AuthPlatform.Application.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthUserRepository _authUserRepository;
        private readonly IAuthUserPermissionRepository _userPermissionRepository;
        private readonly IAuthUserGroupRepository _userGroupRepository;
        private readonly IAuthGroupPermissionRepository _groupPermissionRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        public AuthService(
            IAuthUserRepository authUserRepository,
            IAuthUserPermissionRepository userPermissionRepository,
            IAuthUserGroupRepository userGroupRepository,
            IAuthGroupPermissionRepository groupPermissionRepository,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator)
        {
            _authUserRepository = authUserRepository;
            _userPermissionRepository = userPermissionRepository;
            _userGroupRepository = userGroupRepository;
            _groupPermissionRepository = groupPermissionRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
        }

        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _authUserRepository.GetByUserNameAsync(request.UserName);

            if (user == null)
            {
                return new ApiResponse<LoginResponseDto>
                {
                    Success = false,
                    Message = "Invalid username or password."
                };
            }

            if (user.IsLocked)
            {
                return new ApiResponse<LoginResponseDto>
                {
                    Success = false,
                    Message = "User account is locked."
                };
            }

            var validPassword = _passwordHasher.VerifyPassword(
                request.Password,
                user.PasswordHash);

            if (!validPassword)
            {
                return new ApiResponse<LoginResponseDto>
                {
                    Success = false,
                    Message = "Invalid username or password."
                };
            }

            var userPermissions = await _userPermissionRepository
                .GetByUserIdAsync(user.AuthUserId);

            var userPermissionCodes = userPermissions
                .Select(x => x.AuthPermission.PermissionCode)
                .Distinct()
                .ToList();

            var userGroups = await _userGroupRepository
                .GetByUserIdAsync(user.AuthUserId);

            var groupIds = userGroups
                .Select(x => x.AuthGroupId)
                .Distinct()
                .ToList();

            var groupPermissionCodes = new List<string>();

            foreach (var groupId in groupIds)
            {
                var groupPermissions = await _groupPermissionRepository
                    .GetByGroupIdAsync(groupId);

                groupPermissionCodes.AddRange(
                    groupPermissions
                        .Select(x => x.AuthPermission.PermissionCode));
            }

            groupPermissionCodes = groupPermissionCodes
                .Distinct()
                .ToList();

            var allPermissionCodes = userPermissionCodes
                .Union(groupPermissionCodes)
                .Distinct()
                .ToList();

            var accessToken = _tokenGenerator.GenerateAccessToken(
                user.AuthUserId,
                user.UserName,
                allPermissionCodes);

            var refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

            return new ApiResponse<LoginResponseDto>
            {
                Success = true,
                Message = "Login successful.",
                Data = new LoginResponseDto
                {
                    AuthUserId = user.AuthUserId,
                    FirstName = user.FirstName,
                    UserName = user.UserName,
                    Email = user.Email,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    AccessTokenExpiration = DateTime.UtcNow.AddMinutes(30),
                    Permissions = userPermissionCodes,
                    GroupPermissions = groupPermissionCodes
                }
            };
        }

        public async Task<ApiResponse<string>> RegisterAsync(RegisterUserDto request)
        {
            var existingUser = await _authUserRepository
                .GetByUserNameAsync(request.UserName);

            if (existingUser != null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Username already exists."
                };
            }

            var passwordHash = _passwordHasher.HashPassword(request.Password);

            var user = new Domain.Auth.Entities.AuthUser
            {
                FirstName = request.FirstName,
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash
            };

            await _authUserRepository.AddAsync(user);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "User registered successfully."
            };
        }

        public Task<ApiResponse<LoginResponseDto>> RefreshTokenAsync(
            RefreshTokenRequestDto request)
        {
            return Task.FromResult(new ApiResponse<LoginResponseDto>
            {
                Success = true,
                Message = "Refresh token workflow ready."
            });
        }

        public Task<ApiResponse<string>> LogoutAsync(string refreshToken)
        {
            return Task.FromResult(new ApiResponse<string>
            {
                Success = true,
                Message = "Logout successful."
            });
        }
    }
}
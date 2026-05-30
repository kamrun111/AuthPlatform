using AuthPlatform.Mvc.Models;
using AuthPlatform.Mvc.Models.Auth;
using AuthPlatform.Mvc.Session;

namespace AuthPlatform.Mvc.Services
{
    public class AuthService
    {
        private readonly ApiClientService _apiClient;
        private readonly TokenSessionManager _tokenSessionManager;


        public AuthService(ApiClientService apiClient, TokenSessionManager tokenSessionManager)
        {
            _apiClient = apiClient;
            _tokenSessionManager = tokenSessionManager;
        }

        public async Task<ApiResponse<LoginResponseViewModel>?> LoginAsync(LoginViewModel model)
        {
            var result = await _apiClient.PostAsync<ApiResponse<LoginResponseViewModel>>("auth/login", model);

            if (result?.Success == true && result.Data != null)
            {
                _tokenSessionManager.SetLoginSession(
                    result.Data.AccessToken, 
                    result.Data.RefreshToken,
                    result.Data.AuthUserId, 
                    result.Data.FirstName, 
                    result.Data.LastName,
                    result.Data.UserName, 
                    result.Data.Permissions);
               

                _tokenSessionManager.SetUserPermissions(result.Data.Permissions);

                _tokenSessionManager.SetGroupPermissions(result.Data.GroupPermissions ?? new List<string>());
            }

            return result;
        }

        public void Logout()
        {
            _tokenSessionManager.ClearSession();
        }
    }
}
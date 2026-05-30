using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace AuthPlatform.Mvc.Session;

public class TokenSessionManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenSessionManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext!.Session;

    public void SetLoginSession(string accessToken, string refreshToken, int authUserId, string firstName, string lastName, string userName, List<string> permissions)
    {
        Session.SetString("AccessToken", accessToken);
        Session.SetString("RefreshToken", refreshToken);
        Session.SetInt32("AuthUserId", authUserId);
        Session.SetString("FirstName", firstName);
        Session.SetString("LastName", lastName);
        Session.SetString("UserName", userName);
        Session.SetString("Permissions", JsonSerializer.Serialize(permissions));
    }

    public void SetRolePermissions(List<string> permissions)
    {
        Session.SetString("RolePermissions", JsonSerializer.Serialize(permissions));
    }

    public void SetUserPermissions(List<string> permissions)
    {
        Session.SetString("UserPermissions", JsonSerializer.Serialize(permissions));
    }

    public int? GetAuthUserId()
    {
        return Session.GetInt32("AuthUserId");
    }

    public string? GetAccessToken()
    {
        return Session.GetString("AccessToken");
    }

    public string? GetRefreshToken()
    {
        return Session.GetString("RefreshToken");
    }

    public string? GetFirstName()
    {
        return Session.GetString("FirstName");
    }

    public string? GetLastName()
    {
        return Session.GetString("LastName");
    }

    public string? GetUserName()
    {
        return Session.GetString("UserName");
    }

    public string GetDisplayName()
    {
        var firstName = GetFirstName();
        var lastName = GetLastName();
        var displayName = $"{firstName} {lastName}".Trim();

        if (!string.IsNullOrWhiteSpace(displayName))
        {
            return displayName;
        }

        return GetUserName() ?? "User";
    }

    public List<string> GetRolePermissions()
    {
        var json = Session.GetString("RolePermissions");

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<string>();
        }

        return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
    }

    public List<string> GetUserPermissions()
    {
        var json = Session.GetString("UserPermissions");

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<string>();
        }

        return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
    }

    public bool HasRolePermission(string permissionCode)
    {
        return GetRolePermissions().Contains(permissionCode);
    }

    public bool HasUserPermission(string permissionCode)
    {
        return GetUserPermissions().Contains(permissionCode);
    }

    public bool IsAuthenticated()
    {
        return !string.IsNullOrWhiteSpace(GetAccessToken());
    }

    public void ClearSession()
    {
        Session.Clear();
    }
}
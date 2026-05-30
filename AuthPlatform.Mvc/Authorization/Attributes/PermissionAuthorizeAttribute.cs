using AuthPlatform.Mvc.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthPlatform.Mvc.Authorization.Attributes
{
    public class PermissionAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenSessionManager = context.HttpContext.RequestServices.GetRequiredService<TokenSessionManager>();

            var isAuthenticated = tokenSessionManager.IsAuthenticated();

            if (!isAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            var controllerName = context.RouteData.Values["controller"]?.ToString();
            var actionName = context.RouteData.Values["action"]?.ToString();
            var permissionCode = $"{controllerName}.{actionName}";

            var hasRolePermission = tokenSessionManager.HasRolePermission(permissionCode);
            var hasUserPermission = tokenSessionManager.HasUserPermission(permissionCode);

            if (!hasRolePermission && !hasUserPermission)
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Auth", null);
            }
        }
    }
}
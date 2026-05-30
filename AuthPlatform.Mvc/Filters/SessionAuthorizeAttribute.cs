
using AuthPlatform.Mvc.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthPlatform.Mvc.Filters
{


   

    public class SessionAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(
            AuthorizationFilterContext context)
        {
            var tokenSessionManager =
                context.HttpContext.RequestServices
                    .GetRequiredService<TokenSessionManager>();

            var isAuthenticated =
                tokenSessionManager.IsAuthenticated();

            if (!isAuthenticated)
            {
                context.Result = new RedirectToActionResult(
                    "Login",
                    "Auth",
                    null);
            }
        }
    }
}

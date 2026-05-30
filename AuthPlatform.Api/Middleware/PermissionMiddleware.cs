using AuthPlatform.Api.Attributes;

namespace AuthPlatform.Api.Middleware
{
    //This middleware reads [HasPermission] from the endpoint and checks if JWT contains that permission claim.

    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            var permissionAttribute = endpoint?
                .Metadata
                .GetMetadata<HasPermissionAttribute>();

            if (permissionAttribute == null)
            {
                await _next(context);
                return;
            }

            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            var hasPermission = context.User.Claims.Any(c =>
                c.Type == "permission" &&
                c.Value == permissionAttribute.PermissionCode);

            if (!hasPermission)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Permission denied");
                return;
            }

            await _next(context);
        }
    }
}

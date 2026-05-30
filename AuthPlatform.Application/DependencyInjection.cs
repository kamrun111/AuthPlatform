using AuthPlatform.Application.Auth.Interfaces;
using AuthPlatform.Application.Auth.Services;
using AuthPlatform.Application.Expenditures.Interfaces;
using AuthPlatform.Application.Expenditures.Services;
using Microsoft.Extensions.DependencyInjection;


namespace AuthPlatform.Application
{




    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Auth
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IAuthPermissionService, PermissionService>();

            // Expenditures
            services.AddScoped<IExpenditureHeadService, ExpenditureHeadService>();
            services.AddScoped<IExpenditureInvoiceService, ExpenditureInvoiceService>();
            services.AddScoped<IExpenditureInvoiceDetailService, ExpenditureInvoiceDetailService>();
            services.AddScoped<IExpenditureHeadService, ExpenditureHeadService>();

            return services;
        }
    }
}

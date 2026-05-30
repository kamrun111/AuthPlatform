using AuthPlatform.Domain.Auth.Interfaces;
using AuthPlatform.Infrastructure.Auth.EF;
using AuthPlatform.Infrastructure.Common.Connections;
using AuthPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthPlatform.Domain.Expenditures.Interfaces;
using AuthPlatform.Infrastructure.Expenditures.EF;
using AuthPlatform.Application.Auth.Security;
using AuthPlatform.Infrastructure.Security;




namespace AuthPlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Dapper
        services.AddScoped<DapperContext>();

        // Security
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<ITokenGenerator, TokenGenerator>();

        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();

        // Repositories
        services.AddScoped<IAuthUserRepository, AuthUserRepository>();

        services.AddScoped<IAuthGroupRepository, AuthGroupRepository>();

        services.AddScoped<IAuthPermissionRepository, AuthPermissionRepository>();

        services.AddScoped<IAuthUserPermissionRepository, AuthUserPermissionRepository>();

        services.AddScoped<IAuthGroupPermissionRepository, AuthGroupPermissionRepository>();
        services.AddScoped<IAuthGroupRepository, AuthGroupRepository>();

        services.AddScoped<IAuthPermissionRepository, AuthPermissionRepository>();

        services.AddScoped<IAuthUserPermissionRepository, AuthUserPermissionRepository>();

        services.AddScoped<IAuthGroupPermissionRepository, AuthGroupPermissionRepository>();

        services.AddScoped<IExpenditureHeadRepository, ExpenditureHeadRepository>();
        services.AddScoped<IExpenditureInvoiceRepository,ExpenditureInvoiceRepository>();

        services.AddScoped<IExpenditureInvoiceDetailRepository,ExpenditureInvoiceDetailRepository>();

        return services;
    }
}
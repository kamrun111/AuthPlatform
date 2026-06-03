using System.Text;
using AuthPlatform.Api.Middleware;
using AuthPlatform.Application.Auth.Mappings;
using AuthPlatform.Application.Expenditures.Mappings;
using AuthPlatform.Infrastructure;
using AuthPlatform.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using AuthPlatform.Application.Auth.Interfaces;
using AuthPlatform.Application.Auth.Services;
using AuthPlatform.Application.Expenditures.Interfaces;
using AuthPlatform.Application.Expenditures.Services;
using AuthPlatform.Domain.Auth.Interfaces;
using AuthPlatform.Infrastructure.Auth.EF;
using AuthPlatform.Infrastructure.Auth.EF.AuthPlatform.Infrastructure.Auth.EF;

var builder = WebApplication.CreateBuilder(args);

#region SERVICES

builder.Services.AddControllers();

#endregion

#region DB CONTEXT (FIXED)

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure()));

#endregion

#region DEPENDENCY INJECTION

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IExpenditureHeadService, ExpenditureHeadService>();
builder.Services.AddScoped<IExpenditureInvoiceService, ExpenditureInvoiceService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IAuthGroupRepository, AuthGroupRepository>();
builder.Services.AddScoped<IAuthPermissionService, PermissionService>();
builder.Services.AddScoped<IAuthPermissionRepository, AuthPermissionRepository>();
builder.Services.AddScoped<IAuthUserPermissionRepository, AuthUserPermissionRepository>();
builder.Services.AddScoped<IAuthGroupPermissionRepository, AuthGroupPermissionRepository>();
builder.Services.AddScoped<IAuthUserGroupRepository, AuthUserGroupRepository>();
builder.Services.AddScoped<IAuthUserService, AuthUserService>();

#endregion

#region AUTOMAPPER

builder.Services.AddAutoMapper(typeof(AuthMappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ExpenditureMappingProfile).Assembly);

#endregion

#region JWT

var jwtSecretKey = builder.Configuration["JwtSettings:SecretKey"];

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecretKey!)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

#endregion

#region SWAGGER

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AuthPlatform API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter JWT token like this: Bearer your_token_here",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

#endregion

var app = builder.Build();

#region MIDDLEWARE

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<PermissionMiddleware>();

app.MapControllers();

#endregion

app.Run();
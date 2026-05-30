using AuthPlatform.Mvc.Reports.Services;
using AuthPlatform.Mvc.Services;
using AuthPlatform.Mvc.Session;
using AuthPlatform.Mvc.Reports.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// HttpContext / Session
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);

    options.Cookie.HttpOnly = true;

    options.Cookie.IsEssential = true;

    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    options.Cookie.SameSite = SameSiteMode.Strict;
});

// Application Services
builder.Services.AddScoped<TokenSessionManager>();

builder.Services.AddScoped<AuthService>();

// API Clients
builder.Services.AddHttpClient<ApiClientService>();

builder.Services.AddHttpClient<ApiClientSession>();

// Report
builder.Services.AddScoped<IFastReportService, FastReportService>();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
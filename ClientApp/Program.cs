// Prod-ready Program.cs örneği
using Microsoft.EntityFrameworkCore;
using ClientApp.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;  // 5 hatalı denemeden sonra kilitler
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.HttpOnly = true; // JS erişimi engellendi
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Sadece HTTPS
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict; // CSRF koruması
});

var app = builder.Build();

// Prod HTTPS ve HSTS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // tarayıcıda HTTPS enforce
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Basit CSP header ve XSS önlemi
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'");
    await next();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Admin seed
await app.Services.SeedAdminAsync();

// Route mapping
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
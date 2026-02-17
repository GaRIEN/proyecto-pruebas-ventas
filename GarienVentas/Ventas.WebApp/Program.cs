using Microsoft.AspNetCore.Authentication.Cookies;
using Ventas.WebApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. CONFIGURAR AUTENTICACIÓN POR COOKIES
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

// 2. REGISTRAR TUS API CLIENTS
builder.Services.AddApiClients(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// 4. CONFIGURAR LA RUTA POR DEFECTO
// Ahora que el Login tiene [Route("/")], esta configuración se usa 
// principalmente como respaldo o para otras páginas como el Home.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
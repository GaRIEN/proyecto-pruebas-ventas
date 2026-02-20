using Microsoft.AspNetCore.Authentication.Cookies;
using Ventas.WebApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. CONFIGURAR AUTENTICACIÓN POR COOKIES
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // 1. Rutas básicas
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";

        // 2. Tiempos de vida
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Duración de la sesión
        options.SlidingExpiration = true; // Si el usuario navega a los 40 min, se reinicia a 60 min

        // 3. SEGURIDAD CRÍTICA (Evita que te roben el token)
        options.Cookie.HttpOnly = true; // JS no podrá leer la cookie, solo el servidor
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Solo viaja por HTTPS
        options.Cookie.SameSite = SameSiteMode.Lax; // Protección básica contra ataques CSRF

        options.Cookie.Name = "Ventas.AuthToken"; // Nombre personalizado para la cookie

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
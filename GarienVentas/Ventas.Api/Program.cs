using Microsoft.EntityFrameworkCore;
using Ventas.Api.Endpoints.Base;
using Ventas.Application.Queries.ClientsQueries;
using Ventas.Core.Repositories;
using Ventas.Infraestructure.Data;
using Ventas.Infraestructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// --------------------
// Add services
// --------------------

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ DbContext (CONEXIÓN A SQL SERVER)
builder.Services.AddDbContext<VentasDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("VentasDb"));
});

// ✅ Repositories
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// ✅ MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetAllClientsQuery).Assembly);
});

var app = builder.Build();

// --------------------
// Middleware
// --------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Minimal APIs
EndpointsRegistrar.RegisterEndpoints(app);

app.MapControllers();

app.Run();

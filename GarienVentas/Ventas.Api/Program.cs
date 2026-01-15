using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// --------------------
// Add services
// --------------------

builder.Services.AddControllers();

// OpenAPI / Swagger
builder.Services.AddOpenApi();

//// DbContext → lee desde appsettings / appsettings.Development
//builder.Services.AddDbContext<VentasDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("VentasDb")
//    )
//);


var app = builder.Build();

// --------------------
// Middleware
// --------------------

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

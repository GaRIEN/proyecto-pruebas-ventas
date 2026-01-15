@echo off
echo Generando modelos desde la base de datos...

dotnet ef dbcontext scaffold "Server=GARIEN-DESKTOP;Database=VentasDb;Trusted_Connection=True;TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer ^
  --output-dir Entities\Models ^
  --context VentasDbContext ^
  --force ^
  --data-annotations ^
  --namespace Ventas.Core.Entities.Models

echo ¡Listo! Modelos generados correctamente.
pause


using Ventas.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Ventas.Infraestructure.Data;

public partial class VentasDbContext : DbContext
{
    public VentasDbContext()
    {
    }

    public VentasDbContext(DbContextOptions<VentasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__clients__81A2CBE1D3F53EBE");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07EA05433A");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07D3AD5A48");

            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

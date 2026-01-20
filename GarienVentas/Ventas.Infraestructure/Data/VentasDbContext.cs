using System;
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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__clients__81A2CBE1D3F53EBE");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

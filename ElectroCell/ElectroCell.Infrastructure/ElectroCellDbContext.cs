using ElectroCell.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElectroCell.Infrastructure;

public class ElectroCellDbContext : DbContext
{
    public ElectroCellDbContext(DbContextOptions<ElectroCellDbContext> options) : base(options) { }

    // Aquí se registran las clases que irán a la base de datos
    public DbSet<Producto> Productos => Set<Producto>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Producto>(e =>
        {
            e.Property(p => p.Nombre).HasMaxLength(100).IsRequired();
            e.Property(p => p.Descripcion).HasMaxLength(200);
            e.Property(p => p.Color).HasMaxLength(7);
            e.Property(p => p.Precio).HasPrecision(10, 2);
            e.HasData(
                new Producto { Id = 1, Nombre = "Galaxy A15", Descripcion = "128 GB · 4 GB RAM", Tipo = TipoProducto.Nuevo, Precio = 149, Color = "#1F4BFF" },
                new Producto { Id = 2, Nombre = "Redmi Note 13", Descripcion = "256 GB · 8 GB RAM", Tipo = TipoProducto.Nuevo, Precio = 189, Color = "#2FB67A" },
                new Producto { Id = 3, Nombre = "Moto G54", Descripcion = "128 GB · 8 GB RAM", Tipo = TipoProducto.Nuevo, Precio = 159, Color = "#7A4BFF" },
                new Producto { Id = 4, Nombre = "iPhone 11", Descripcion = "64 GB · batería 90%+", Tipo = TipoProducto.Seminuevo, Precio = 239, Color = "#E5484D" },
                new Producto { Id = 5, Nombre = "iPhone 12", Descripcion = "128 GB · batería 90%+", Tipo = TipoProducto.Seminuevo, Precio = 329, Color = "#0E1A2B" },
                new Producto { Id = 6, Nombre = "Galaxy S21", Descripcion = "128 GB · como nuevo", Tipo = TipoProducto.Seminuevo, Precio = 279, Color = "#C06BD6" },
                new Producto { Id = 7, Nombre = "Cargador rápido 25W", Descripcion = "USB-C · 1 año de garantía", Tipo = TipoProducto.Accesorio, Precio = 15, Color = "#FFB020" },
                new Producto { Id = 8, Nombre = "Funda + vidrio templado", Descripcion = "Para tu modelo", Tipo = TipoProducto.Accesorio, Precio = 10, Color = "#52627A" });
        });
    }
}

// Permite usar "dotnet ef migrations add" desde esta capa
public class ElectroCellDbContextFactory : IDesignTimeDbContextFactory<ElectroCellDbContext>
{
    public ElectroCellDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<ElectroCellDbContext>().UseSqlServer(Conexion.Default).Options;
        return new ElectroCellDbContext(options);
    }
}

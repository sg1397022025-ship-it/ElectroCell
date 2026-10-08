using ElectroCell.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ElectroCell.Services;

// Así la capa de presentación no necesita conocer la capa de datos
public static class ServiceFactory
{
    public static ProductoService CrearProductoService()
    {
        var options = new DbContextOptionsBuilder<ElectroCellDbContext>().UseSqlServer(Conexion.Default).Options;
        var db = new ElectroCellDbContext(options);
        db.Database.EnsureCreated(); // crea la BD y los datos de ejemplo la primera vez
        return new ProductoService(db);
    }
}

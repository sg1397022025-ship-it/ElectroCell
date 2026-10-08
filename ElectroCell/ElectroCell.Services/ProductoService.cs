using System.Text.RegularExpressions;
using ElectroCell.Domain;
using ElectroCell.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ElectroCell.Services;

public class ProductoService
{
    private readonly ElectroCellDbContext _db;

    public ProductoService(ElectroCellDbContext db) => _db = db;

    public List<Producto> Listar(TipoProducto? tipo = null) =>
        _db.Productos.AsNoTracking()
            .Where(p => tipo == null || p.Tipo == tipo)
            .OrderBy(p => p.Tipo).ThenBy(p => p.Nombre)
            .ToList();

    public void Guardar(Producto p)
    {
        Validar(p); // antes de guardar en db
        if (p.Id == 0) _db.Productos.Add(p); else _db.Productos.Update(p);
        _db.SaveChanges();
        _db.ChangeTracker.Clear();
    }

    public void Eliminar(int id)
    {
        var p = _db.Productos.Find(id) ?? throw new ServiceException("El producto no existe.");
        _db.Productos.Remove(p);
        _db.SaveChanges();
        _db.ChangeTracker.Clear();
    }

    private void Validar(Producto p)
    {
        p.Nombre = (p.Nombre ?? "").Trim();
        if (p.Nombre.Length == 0) throw new ServiceException("El nombre es obligatorio.");
        if (p.Nombre.Length > 100) throw new ServiceException("El nombre no puede pasar de 100 caracteres.");
        if ((p.Descripcion ?? "").Length > 200) throw new ServiceException("La descripción no puede pasar de 200 caracteres.");
        if (p.Precio <= 0) throw new ServiceException("El precio debe ser mayor que cero.");
        if (!Regex.IsMatch(p.Color ?? "", "^#[0-9A-Fa-f]{6}$")) throw new ServiceException("El color no es válido.");
        if (_db.Productos.Any(x => x.Id != p.Id && x.Nombre == p.Nombre))
            throw new ServiceException("Ya existe un producto con ese nombre.");
    }
}

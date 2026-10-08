namespace ElectroCell.Domain;

// POCO: sin dependencias
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public TipoProducto Tipo { get; set; }
    public decimal Precio { get; set; }
    public string Color { get; set; } = "#1F4BFF";
}

using ElectroCell.Domain;
using ElectroCell.Services;

namespace ElectroCell.WinForms;

public class FrmProducto : Form
{
    public Producto Producto { get; }
    readonly ProductoService _svc;
    readonly TextBox _nombre = new(), _desc = new();
    readonly ComboBox _tipo = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    readonly NumericUpDown _precio = new() { DecimalPlaces = 2, Maximum = 100000 };
    readonly Button _color = new() { Text = "Elegir color", FlatStyle = FlatStyle.Flat };
    Color _c;

    public FrmProducto(ProductoService svc, Producto? p)
    {
        _svc = svc;
        Producto = p == null ? new Producto() : new Producto { Id = p.Id, Nombre = p.Nombre, Descripcion = p.Descripcion, Tipo = p.Tipo, Precio = p.Precio, Color = p.Color };
        Text = p == null ? "Nuevo producto" : "Editar producto";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        ClientSize = new Size(380, 410);
        Font = new Font("Segoe UI", 10);

        _tipo.DataSource = Enum.GetValues<TipoProducto>();
        _nombre.Text = Producto.Nombre;
        _desc.Text = Producto.Descripcion;
        _tipo.SelectedItem = Producto.Tipo == 0 ? TipoProducto.Nuevo : Producto.Tipo;
        _precio.Value = Producto.Precio;
        _c = ColorTranslator.FromHtml(Producto.Color);
        PintarColor();
        _color.Click += (_, _) =>
        {
            using var d = new ColorDialog { Color = _c };
            if (d.ShowDialog(this) == DialogResult.OK) { _c = d.Color; PintarColor(); }
        };

        Campo("Nombre", _nombre, 15);
        Campo("Descripción", _desc, 75);
        Campo("Tipo", _tipo, 135);
        Campo("Precio ($)", _precio, 195);
        Campo("Color", _color, 255);
        _color.Height = 30;

        var ok = new Button { Text = "Guardar", Location = new Point(180, 360), Size = new Size(85, 34), BackColor = ColorTranslator.FromHtml("#1F4BFF"), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        var cancel = new Button { Text = "Cancelar", Location = new Point(275, 360), Size = new Size(85, 34), DialogResult = DialogResult.Cancel };
        ok.Click += (_, _) => Aceptar();
        Controls.AddRange(new Control[] { ok, cancel });
        AcceptButton = ok;
        CancelButton = cancel;
    }

    void Campo(string texto, Control c, int y)
    {
        Controls.Add(new Label { Text = texto, Location = new Point(20, y), AutoSize = true });
        c.Location = new Point(20, y + 24);
        c.Width = 340;
        Controls.Add(c);
    }

    void PintarColor()
    {
        _color.BackColor = _c;
        _color.ForeColor = _c.GetBrightness() > 0.55 ? Color.Black : Color.White;
    }

    void Aceptar()
    {
        Producto.Nombre = _nombre.Text;
        Producto.Descripcion = _desc.Text;
        Producto.Tipo = (TipoProducto)_tipo.SelectedItem!;
        Producto.Precio = _precio.Value;
        Producto.Color = $"#{_c.R:X2}{_c.G:X2}{_c.B:X2}";
        try { _svc.Guardar(Producto); DialogResult = DialogResult.OK; }
        catch (ServiceException ex) { MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}

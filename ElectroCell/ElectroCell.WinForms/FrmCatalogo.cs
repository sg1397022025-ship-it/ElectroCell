using System.Diagnostics;
using ElectroCell.Domain;
using ElectroCell.Services;

namespace ElectroCell.WinForms;

public class FrmCatalogo : Form
{
    static readonly Color Ink = ColorTranslator.FromHtml("#0E1A2B");
    static readonly Color Blue = ColorTranslator.FromHtml("#1F4BFF");
    static readonly Color Amber = ColorTranslator.FromHtml("#FFB020");
    static readonly Color Bg = ColorTranslator.FromHtml("#EEF2F6");
    static readonly Color Mute = ColorTranslator.FromHtml("#52627A");
    const string Tel = "50379634302";

    readonly ProductoService _svc = ServiceFactory.CrearProductoService();
    readonly FlowLayoutPanel _grid = new() { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(16), BackColor = Bg };
    readonly Dictionary<Button, TipoProducto?> _tabs = new();
    TipoProducto? _filtro;
    int _selId;

    public FrmCatalogo()
    {
        Text = "Electro Cell | Celulares, accesorios y reparaciones";
        Size = new Size(1100, 760);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Bg;
        Font = new Font("Segoe UI", 10);

        // Encabezado
        var header = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.White };
        var logo = new FlowLayoutPanel { Location = new Point(20, 12), AutoSize = true };
        logo.Controls.Add(new Label { Text = "electro", AutoSize = true, Margin = Padding.Empty, ForeColor = Ink, Font = new Font("Segoe UI", 18, FontStyle.Bold) });
        logo.Controls.Add(new Label { Text = "cell", AutoSize = true, Margin = Padding.Empty, ForeColor = Blue, Font = new Font("Segoe UI", 18, FontStyle.Bold) });
        header.Controls.Add(logo);

        // Hero
        var hero = new Panel { Dock = DockStyle.Top, Height = 170, BackColor = Bg };
        hero.Controls.Add(new Label { Text = "Tu celular, cargado de buenas ofertas.", Location = new Point(20, 16), Size = new Size(1000, 50), ForeColor = Ink, Font = new Font("Segoe UI", 26, FontStyle.Bold) });
        hero.Controls.Add(new Label { Text = "Celulares nuevos y seminuevos, accesorios y reparación el mismo día. Con garantía por escrito en cada equipo.", Location = new Point(24, 72), Size = new Size(1000, 28), ForeColor = Mute, Font = new Font("Segoe UI", 12) });
        var wa = Boton("Escribir por WhatsApp", Blue, Color.White);
        wa.Location = new Point(24, 112);
        wa.Click += (_, _) => Whatsapp("Hola Electro Cell");
        hero.Controls.Add(wa);

        // Barra: filtros + acciones
        var bar = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 60, Padding = new Padding(20, 12, 20, 0), BackColor = Bg };
        foreach (var (txt, f) in new (string, TipoProducto?)[] { ("Todos", null), ("Nuevos", TipoProducto.Nuevo), ("Seminuevos", TipoProducto.Seminuevo), ("Accesorios", TipoProducto.Accesorio) })
        {
            var b = Boton(txt, Color.White, Ink);
            b.Click += (_, _) => { _filtro = f; Cargar(); };
            _tabs[b] = f;
            bar.Controls.Add(b);
        }
        var agregar = Boton("+ Agregar", Blue, Color.White); agregar.Margin = new Padding(40, 0, 8, 0);
        var editar = Boton("Editar", Color.White, Ink);
        var eliminar = Boton("Eliminar", Color.White, Ink);
        var servicios = Boton("Servicios", Amber, Color.FromArgb(26, 18, 0));
        agregar.Click += (_, _) => Editar(null);
        editar.Click += (_, _) => { var p = Seleccionado(); if (p == null) Aviso(); else Editar(p); };
        eliminar.Click += (_, _) => Quitar();
        servicios.Click += (_, _) => MessageBox.Show(
            "• Reparación el mismo día\n• Garantía por escrito\n• Liberación y configuración\n• Recibimos tu equipo usado",
            "Más que vender celulares", MessageBoxButtons.OK, MessageBoxIcon.Information);
        bar.Controls.AddRange(new Control[] { agregar, editar, eliminar, servicios });

        // Pie
        var footer = new Panel { Dock = DockStyle.Bottom, Height = 40, BackColor = Color.White };
        footer.Controls.Add(new Label { Text = "electrocell · Lun a Sáb 8:00 a 18:00 · Tel. 7963 4302", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, ForeColor = Mute });

        Controls.AddRange(new Control[] { _grid, footer, bar, hero, header });
        Cargar();
    }

    void Cargar()
    {
        foreach (var (b, f) in _tabs)
        {
            bool on = f == _filtro;
            b.BackColor = on ? Ink : Color.White;
            b.ForeColor = on ? Color.White : Ink;
        }
        _grid.SuspendLayout();
        _grid.Controls.Clear();
        foreach (var p in _svc.Listar(_filtro)) _grid.Controls.Add(Tarjeta(p));
        _grid.ResumeLayout();
    }

    Panel Tarjeta(Producto p)
    {
        var c = ColorTranslator.FromHtml(p.Color);
        var card = new Panel { Size = new Size(250, 320), Margin = new Padding(8), BackColor = p.Id == _selId ? ColorTranslator.FromHtml("#DCE5FF") : Color.White };
        var thumb = new Panel { Location = new Point(12, 12), Size = new Size(226, 150), BackColor = Mezclar(c, Bg, .14) };
        var borde = new Panel { Location = new Point(76, 8), Size = new Size(74, 134), BackColor = Ink };
        var pantalla = new Panel { Location = new Point(4, 4), Size = new Size(66, 126), BackColor = c };
        borde.Controls.Add(pantalla);
        thumb.Controls.Add(borde);
        var tag = new Label { Text = p.Tipo.ToString(), AutoSize = true, BackColor = Amber, ForeColor = Color.FromArgb(26, 18, 0), Font = new Font("Segoe UI", 8.5f, FontStyle.Bold), Padding = new Padding(4, 1, 4, 1), Location = new Point(14, 174) };
        var nombre = new Label { Text = p.Nombre, Location = new Point(12, 200), Size = new Size(226, 28), ForeColor = Ink, Font = new Font("Segoe UI", 13, FontStyle.Bold) };
        var desc = new Label { Text = p.Descripcion, Location = new Point(12, 230), Size = new Size(226, 40), ForeColor = Mute };
        var precio = new Label { Text = $"${p.Precio:0.##}", Location = new Point(12, 276), AutoSize = true, ForeColor = Ink, Font = new Font("Segoe UI", 18, FontStyle.Bold) };
        var link = new LinkLabel { Text = "Preguntar", AutoSize = true, Location = new Point(165, 286), LinkColor = Blue, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
        link.LinkClicked += (_, _) => Whatsapp("Hola Electro Cell, me interesa: " + p.Nombre);
        card.Controls.AddRange(new Control[] { thumb, tag, nombre, desc, precio, link });
        foreach (var x in new Control[] { card, thumb, borde, pantalla, tag, nombre, desc, precio })
            x.Click += (_, _) => { _selId = p.Id; Cargar(); };
        return card;
    }

    void Editar(Producto? p)
    {
        using var f = new FrmProducto(_svc, p);
        if (f.ShowDialog(this) == DialogResult.OK) Cargar();
    }

    void Quitar()
    {
        var p = Seleccionado();
        if (p == null) { Aviso(); return; }
        if (MessageBox.Show($"¿Eliminar \"{p.Nombre}\"?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try { _svc.Eliminar(p.Id); _selId = 0; Cargar(); }
        catch (ServiceException ex) { MessageBox.Show(ex.Message); }
    }

    Producto? Seleccionado() => _svc.Listar().FirstOrDefault(x => x.Id == _selId);
    static void Aviso() => MessageBox.Show("Primero haz clic en un producto.", "Electro Cell");

    static void Whatsapp(string msg) =>
        Process.Start(new ProcessStartInfo($"https://wa.me/{Tel}?text={Uri.EscapeDataString(msg)}") { UseShellExecute = true });

    static Color Mezclar(Color a, Color b, double t) => Color.FromArgb(
        (int)(a.R * t + b.R * (1 - t)), (int)(a.G * t + b.G * (1 - t)), (int)(a.B * t + b.B * (1 - t)));

    static Button Boton(string texto, Color bg, Color fg)
    {
        var b = new Button { Text = texto, AutoSize = true, BackColor = bg, ForeColor = fg, FlatStyle = FlatStyle.Flat, Padding = new Padding(10, 3, 10, 3), Cursor = Cursors.Hand, Font = new Font("Segoe UI", 10, FontStyle.Bold), Margin = new Padding(0, 0, 8, 0) };
        b.FlatAppearance.BorderColor = Ink;
        return b;
    }
}

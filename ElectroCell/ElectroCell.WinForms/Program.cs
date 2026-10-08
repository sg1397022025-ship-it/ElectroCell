namespace ElectroCell.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        try { Application.Run(new FrmCatalogo()); }
        catch (Exception ex)
        {
            MessageBox.Show("No se pudo conectar a SQL Server.\n\n" + ex.Message, "Electro Cell",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

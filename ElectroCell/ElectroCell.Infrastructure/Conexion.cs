namespace ElectroCell.Infrastructure;

public static class Conexion
{
    // SQL Server LocalDB (viene con Visual Studio)
    public const string Default =
        "Server=(localdb)\\MSSQLLocalDB;Database=ElectroCellDb;Trusted_Connection=True;TrustServerCertificate=True";
}

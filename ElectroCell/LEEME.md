# Electro Cell (Proyecto final)

Requisitos: .NET 10 SDK, Windows, SQL Server LocalDB (viene con Visual Studio).

1. Abre `ElectroCell.sln` en Visual Studio.
2. La conexión ya apunta a LocalDB: `(localdb)\MSSQLLocalDB`. No necesitas crear la base de datos.
3. Marca `ElectroCell.WinForms` como proyecto de inicio y ejecuta (F5).
   La primera vez se crea la base de datos `ElectroCellDb` con los 8 productos de ejemplo.

Capas:
- Domain (Entity, POCO): sin dependencias
- Infrastructure (DbContext): depende de Domain
- Services (validaciones): depende de Domain e Infrastructure
- WinForms (presentación): usa Domain y Services

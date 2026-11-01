# TiendaUTEC

A product catalog web application with full CRUD functionality, built as a guided university practice project at UTEC. It uses ASP.NET Core MVC and SQL Server, with data access through ADO.NET.

## Features

- Product catalog list showing category, price, and stock
- Product detail view
- Create new products, choosing the category from a dropdown
- Edit existing products
- Delete products with a confirmation page (soft delete: products are marked as inactive instead of being removed from the database)
- Form validation (required name, valid price and stock, required category)

## Tech Stack

- C# and .NET 10
- ASP.NET Core MVC (Razor views)
- SQL Server
- Microsoft.Data.SqlClient (ADO.NET)
- Bootstrap

## Project Structure

```
TiendaUTEC/
├── Controllers/   # ProductosController, HomeController
├── Datos/         # ProductoRepositorio (SQL Server data access)
├── Models/        # Producto, Categoria
├── Views/         # Razor views (Productos/Index, Crear, Editar, Detalle, Eliminar)
└── Program.cs
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (Express or higher) and SQL Server Management Studio

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/Lildani26/TiendaUTEC.git
   cd TiendaUTEC
   ```
2. Create the database by running the script `Scripts/TiendaUTEC.sql` in SSMS.
3. Update the connection string in `appsettings.json` with your server name:
   ```json
   "ConnectionStrings": {
     "TiendaUTEC": "Server=YOUR_SERVER;Database=TiendaUTEC;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
4. Run the project:
   ```bash
   dotnet run
   ```
5. Open the address shown in the console and go to `/Productos`.

## Author

Brandon Daniel Flores Ayala
Systems Engineering student at UTEC

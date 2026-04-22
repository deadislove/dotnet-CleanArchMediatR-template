# Database & Migrations Guide

This template uses a **Persistence Factory** pattern to support multiple database providers (SQL Server, PostgreSQL, SQLite).

## Supported Providers
The provider is determined by the `DatabaseSettings:Provider` key in your `appsettings.json`.

- `MSSQL`: Microsoft SQL Server
- `PostgreSQL`: PostgreSQL
- `SQLite`: SQLite (Default for development)

## Creating a Migration

Because the `DbContext` is located in the **Infra** layer and the startup project is the **Api** layer, you must specify both when running EF Core commands.

### 1. Add a new Migration
Open your terminal in the root folder and run:
```bash
dotnet ef migrations add InitialCreate \
    --project src/CleanArchMediatR.Template.Infra \
    --startup-project src/CleanArchMediatR.Template.Api \
    --output-dir Data/Migrations
```

### 2. Update the Database
To apply the migrations to your configured database:
```bash
dotnet ef database update \
    --project src/CleanArchMediatR.Template.Infra \
    --startup-project src/CleanArchMediatR.Template.Api
```

## Switching Providers
1.  Modify `appsettings.json`:
    ```json
    "DatabaseSettings": {
      "Provider": "PostgreSQL",
      "ConnectionString": "Host=localhost;Database=mydb;Username=postgres;Password=pwd"
    }
    ```
2.  Ensure you have the corresponding Docker container running (see `docker-compose.postgresql.yml`).
3.  If switching between providers with different schemas, you may need to create provider-specific migrations.

## Production Note
In the current `Program.cs`, `app.Database.EnsureCreated()` is used for development convenience. 

> [!WARNING]
> For **Production environments**, it is highly recommended to use `app.Database.Migrate()` instead, or handle migrations as part of your CI/CD pipeline to ensure data integrity and support future schema changes.

## Troubleshooting
- **Missing Tools**: Ensure you have the EF Core CLI tool installed: `dotnet tool install --global dotnet-ef`.
- **Connection Strings**: Double-check the connection string in `appsettings.Development.json` if the database fails to connect.
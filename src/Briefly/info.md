# Entity Framework Core Migrations Guide

## Adding a Migration

To add a migration for the `NotesDbContext`, use the following command:

```bash
dotnet ef migrations add "Initial migration" --project ..\migrations\Briefly.Migrations --context NotesDbContext -o Notes
```


### Explanation of the Command:
- **`add "Initial migration"`**: Adds a new migration named "Initial migration".
- **`--project`**: Specifies the project where the migration files will be created. In this case, the project is located at `C:\Work\sources\Anetlab\briefly\src\Briefly\migrations\Briefly.Migrations`.
- **`--context NotesDbContext`**: Specifies the `DbContext` for which the migration is being created.
- **`-o Notes`**: Specifies the output directory for the migration files within the project.

---

## Configuring the Migrations Assembly

In the `Extensions.cs` file, the `DbContext` is configured to use the `Briefly.Migrations` assembly for migrations. This is done using the `MigrationsAssembly` method:

```csharp
options.UseNpgsql(pgConnectionString, b => b.MigrationsAssembly(typeof(MigrationsMetaData).Assembly.GetName().Name));
```
### Key Points:
- **`pgConnectionString`**: The PostgreSQL connection string is retrieved from the configuration.
- **`MigrationsAssembly`**: Specifies the assembly where migrations are stored. In this case, it uses the assembly containing the `MigrationsMetaData` class.

---

## Placeholder Connection String for Migrations

If the PostgreSQL connection string is not configured, you can use a placeholder connection string to generate migrations without connecting to a live database. For example:
```jsson
{ "ConnectionStrings": { "briefly-platform-db": "Host=localhost;Database=PlaceholderDb;Username=placeholder;Password=placeholder" } }
```

---

## Common Errors and Fixes

### 1. **Error: PostgreSQL connection string is not configured**
   - Ensure the connection string is set in `appsettings.json` or `appsettings.Development.json`.
   - Use a placeholder connection string if a live database is not required.

### 2. **Error: Your target project doesn't match your migrations assembly**
   - Ensure the `MigrationsAssembly` is correctly configured in the `DbContext` registration:
   ```csharp
options.UseNpgsql(pgConnectionString, b => b.MigrationsAssembly("Briefly.Migrations"));
   ```
   
     
---

## Automatic Migration Application

In the `UseBrieflyFramework` method, pending migrations are applied automatically at runtime:
```csharp
using (var scope = app.Services.CreateScope()) { var dbContext = scope.ServiceProvider.GetRequiredService<NotesDbContext>(); dbContext.Database.Migrate(); }
```


### Key Points:
- **`CreateScope`**: Creates a scoped service provider to resolve the `DbContext`.
- **`Database.Migrate()`**: Applies any pending migrations to the database.

---

## Additional Notes

- Ensure the `dotnet-ef` tool is installed globally:
```bash
dotnet tool install --global dotnet-ef
```
- Restore dependencies before running migration commands:

```bash
dotnet restore
``` 
  
- Use the `--msbuildprojectextensionspath` option if custom paths are used for intermediate output.

---

  
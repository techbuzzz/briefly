# Entity Framework Core Migrations Guide


## Adding a Migration

To add a migration for the `NotesDbContext`, use the following command. **Run this command from the folder where `Briefly.sln` is located:**
dotnet ef migrations add AddTitleToNotes \
  --project Briefly.Migrations/Briefly.Migrations.csproj \
  --startup-project api/Briefly.Server/Briefly.Server.csproj \
  --context NotesDbContext \
  -o Notes
### Explanation of the Command:
- **`add AddTitleToNotes`**: Adds a new migration named "AddTitleToNotes" (replace with your migration name).
- **`--project`**: Specifies the project where the migration files will be created. Here, it is `Briefly.Migrations/Briefly.Migrations.csproj`.
- **`--startup-project`**: Specifies the startup project to use for configuration and dependency injection. Here, it is `api/Briefly.Server/Briefly.Server.csproj`.
- **`--context NotesDbContext`**: Specifies the `DbContext` for which the migration is being created.
- **`-o Notes`**: Specifies the output directory for the migration files within the migrations project.

> **Note:** You must run the migration command from the directory containing `Briefly.sln`.

---

## Configuring the Migrations Assembly

The `DbContext` is configured to use the `Briefly.Migrations` assembly for migrations. This is set in `api/Briefly.Infrastructure/Persistence/Extensions.cs`:
options.UseNpgsql(connectionString, e => e.MigrationsAssembly("Briefly.Migrations"));
- In DEBUG builds, the migrations history table is set to `MigrationsHistory` in the `ef` schema.
- The connection string is injected via `DatabaseOptions` and configured in `api/Briefly.Infrastructure/Extensions.cs`.

---

## Connection String Configuration

The PostgreSQL connection string is set in your configuration (e.g., `appsettings.json`) under the key `briefly-platform-db`. It is loaded in `api/Briefly.Infrastructure/Extensions.cs`:
var pgConnectionString = builder.Configuration.GetConnectionString("briefly-platform-db");
If the connection string is not set, an exception is thrown at startup. For migration generation without a live database, use a placeholder connection string:
{
  "ConnectionStrings": {
    "briefly-platform-db": "Host=localhost;Database=PlaceholderDb;Username=placeholder;Password=placeholder"
  }
}
---

## Automatic Migration and Seeding

At runtime, all `IDbInitializer` implementations are called to apply pending migrations and seed data. This is handled in `api/Briefly.Infrastructure/Extensions.cs`:
private static void SetupDatabases(this IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.CreateScope();
    var initializers = scope.ServiceProvider.GetServices<IDbInitializer>();
    foreach (var initializer in initializers)
    {
        initializer.MigrateAsync(CancellationToken.None).Wait();
        initializer.SeedAsync(CancellationToken.None).Wait();
    }
}
- `NotesDbInitializer` in `modules/Notes/Notes.Application/Persistence/NotesDbInitializer.cs` handles migration and seeding for `NotesDbContext`.

---

## Additional Notes

- Ensure the `dotnet-ef` tool is installed globally:
dotnet tool install --global dotnet-ef- Restore dependencies before running migration commands:
dotnet restore- Use the `--msbuildprojectextensionspath` option if custom paths are used for intermediate output.

---

## Common Errors and Fixes

### 1. **Error: PostgreSQL connection string is not configured**
   - Ensure the connection string is set in your configuration files.
   - Use a placeholder connection string if a live database is not required.

### 2. **Error: Your target project doesn't match your migrations assembly**
   - Ensure the `MigrationsAssembly` is set to `"Briefly.Migrations"` in the `DbContext` registration.

---

## Project Structure Reference
- **DbContext:** `modules/Notes/Notes.Application/Persistence/NotesDbContext.cs`
- **Migrations Project:** `Briefly.Migrations`
- **Migration Output Directory:** `Notes` (within `Briefly.Migrations`)
- **Migrations Assembly:** `Briefly.Migrations`
- **Initializer:** `modules/Notes/Notes.Application/Persistence/NotesDbInitializer.cs`

---
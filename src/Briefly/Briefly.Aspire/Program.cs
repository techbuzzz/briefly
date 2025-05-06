using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("briefly-platform-db")
    .WithImage("postgres:latest")
    // .WithContainerName("postgres-briefly")
    .WithVolume("pgdata-briefly", "/var/lib/postgresql/briefly-data")
    .WithPgAdmin();

// Configure Redis using Aspire's AddRedis method
var redis = builder.AddRedis("briefly-platform-cache");
    // .WithContainerName("cache-briefly");

var server = builder.AddProject<Briefly_Server>("briefly-server")
    .WithReference(postgres)
    .WithReference(redis)
    .WaitFor(postgres)
    .WaitFor(redis);

builder.AddProject<Briefly_Client>("briefly-wasm")
    .WaitFor(server);

builder.Build().Run();
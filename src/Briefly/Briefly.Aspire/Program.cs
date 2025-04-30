var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("briefly-platform-db")
    .WithImage("postgres:latest")
    .WithVolume("pgdata-briefly", "/var/lib/postgresql/briefly-data")
    .WithPgAdmin();

// Configure Redis using Aspire's AddRedis method
var redis = builder.AddRedis("briefly-platform-cache");

builder.AddProject<Projects.Briefly_Server>("briefly-server")
    .WithReference(postgres)
    .WithReference(redis)
    .WaitFor(postgres)
    .WaitFor(redis); ;

builder.Build().Run();

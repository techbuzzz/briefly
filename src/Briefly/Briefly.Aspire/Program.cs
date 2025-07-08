using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Configure Redis using Aspire's AddRedis method
var redis = builder.AddRedis("briefly-platform-cache");
    // .WithContainerName("cache-briefly");

var server = builder.AddProject<Briefly_Server>("briefly-server")
    .WithReference(redis)
    .WaitFor(redis);

builder.AddProject<Briefly_Client>("briefly-wasm")
    .WaitFor(server);

builder.Build().Run();
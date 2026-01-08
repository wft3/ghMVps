using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);
//var scalar = builder.AddScalarApiReference();

builder.AddProject<Projects.Dashboard>("dashboard");

var apiApp = builder.AddProject<Projects.Api>("api");

//scalar.WithApiReference(apiApp);

builder.Build().Run();

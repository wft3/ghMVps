using Api.ApiEndPoints;
using Api.Context;
using Api.Extensions;
using Common.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register your DbContexts
string _dashboardConnection;
string _mvpsConnection;
string _samsConnection;
using (DecryptionService decryptionService = new DecryptionService(new LoggerFactory()))
{
    _dashboardConnection = decryptionService.Decrypt(builder.Configuration.GetConnectionString("MvpsDashboardDb") ?? throw new InvalidOperationException("Connection string"
        + "'_dashboardConnection' not found."));
    _mvpsConnection = decryptionService.Decrypt(builder.Configuration.GetConnectionString("MvpsDb") ?? throw new InvalidOperationException("Connection string"
        + "'_mvpsConnection' not found."));
    _samsConnection = decryptionService.Decrypt(builder.Configuration.GetConnectionString("SamsDb") ?? throw new InvalidOperationException("Connection string"
        + "'_samsConnection' not found."));
}

builder.Services.AddDbContext<MvpsDbContext>(options =>
    options.UseSqlServer(_mvpsConnection));
builder.Services.AddDbContext<MvpsDashboardDbContext>(options =>
    options.UseSqlServer(_dashboardConnection));
builder.Services.AddDbContext<SamsDbContext>(options =>
    options.UseSqlServer(_samsConnection));

//Register Services
RegisterServicesExtension.RegisterServices(builder.Services);
// Repository Registration
RegisterRepositoryExtension.RegisterRepositories(builder.Services);




var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();    
}

app.UseHttpsRedirection();

// Register Api Endpoints
app.RegisterHealthEndPoints();
app.RegisterAuthorizationEndPoints();
app.RegisterLookupsEndPoints();
app.RegisterUserEndPoints();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

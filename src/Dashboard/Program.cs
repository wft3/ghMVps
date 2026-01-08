using Dashboard.Authentication;
using Dashboard.Components;
using Dashboard.Services;
using Dashboard.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

#if DEBUG
builder.Services.AddSassCompiler();
#endif

builder.Services.AddHttpClient("ClientWithUntrustedSSL")
    .ConfigureHttpClient(c =>
    {
        var apiUrl = builder.Configuration["MvpsPlusApiUrl"];
        if (string.IsNullOrWhiteSpace(apiUrl))
        {
            throw new InvalidOperationException("MvpsPlusApiUrl configuration value is missing or empty.");
        }
        c.BaseAddress = new Uri(apiUrl);
    })
    .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
    {
        Credentials = CredentialCache.DefaultNetworkCredentials,
        SslOptions = new System.Net.Security.SslClientAuthenticationOptions
        {
            RemoteCertificateValidationCallback = (message, cert, chain, errors) => true
        }
    });
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<CustomAuthProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthProvider>());

// Add authorization
builder.Services.AddAuthorization();
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

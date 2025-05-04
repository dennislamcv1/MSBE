// See https://aka.ms/new-console-template for more information
using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbConnection>(_ =>
    new SqliteConnection("Data Source=safevault.db"));

builder.Services.AddScoped<IDbConnection>(_ =>
    new SqliteConnection("Data Source=safevault.db"));

builder.Services.AddScoped<AuthService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireUser", policy => policy.RequireRole("User", "Admin"));
    options.AddPolicy("RequireGuest", policy => policy.RequireRole("Guest", "User", "Admin"));
});


var app = builder.Build();

app.MapControllers(); // if using [ApiController]
app.Run();




Console.WriteLine("Hello, World!");

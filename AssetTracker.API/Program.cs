using AssetTracker.Infrastructure.Persistence; // Import namespace
using Microsoft.EntityFrameworkCore;
using AssetTracker.Application.Services;
using AssetTracker.Application.Interfaces;
using AssetTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AssetTracker.API.Controllers.AssetsController).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAssetService, AssetService>();

// --- DATABASE REGISTRATION START ---
// This tells .NET: "When someone asks for AppDbContext, give them one connected to SQL Server"
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// --- DATABASE REGISTRATION END ---

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Debug: Listar todas as rotas registradas no console ao iniciar
if (app.Environment.IsDevelopment())
{
    app.Lifetime.ApplicationStarted.Register(() =>
    {
        using var scope = app.Services.CreateScope();
        var endpointSources = scope.ServiceProvider.GetServices<EndpointDataSource>();
        foreach (var source in endpointSources)
        {
            foreach (var endpoint in source.Endpoints)
            {
                Console.WriteLine($"[ROUTE]: {endpoint.DisplayName} -> {endpoint.Metadata.GetMetadata<Microsoft.AspNetCore.Routing.RouteEndpoint>()?.RoutePattern.RawText}");
            }
        }
    });
}

app.Run();
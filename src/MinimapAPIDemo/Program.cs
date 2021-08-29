using MinimapAPIDemo;
using Microsoft.AspNetCore.Builder;
using MinimapAPIDemo.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.MapApi();
app.MapHealthChecks("/health");
app.MapInfrastructure();

app.UseInfrastructure();

await app.RunAsync();
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddGraphQLServer();

var app = builder.Build();
app.MapHealthChecks("/health");
app.MapGraphQL();

await app.RunAsync();

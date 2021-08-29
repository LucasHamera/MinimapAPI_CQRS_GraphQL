using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddGraphQLServer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API demo", Version = "v1" });
});

var app = builder.Build();
app.MapHealthChecks("/health");
app.MapGraphQL();

app.UseSwagger();
app.UseReDoc(reDoc =>
{
    reDoc.RoutePrefix = "docs";
    reDoc.SpecUrl("/swagger/v1/swagger.json");
    reDoc.DocumentTitle = "Minimal API demo v1";
});

await app.RunAsync();

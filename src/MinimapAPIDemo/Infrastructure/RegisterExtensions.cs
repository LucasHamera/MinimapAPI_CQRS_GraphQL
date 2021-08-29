using MediatR;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MinimapAPIDemo.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MinimapAPIDemo.Infrastructure;
internal static class RegisterExtensions
{
    private const string ConnectionStringName = "TodoDatabase";

    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGraphQLServer();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API demo", Version = "v1" });
        });
        services.AddMediatR(typeof(MediatRAssembly));
        services
            .AddDbContext<TodoContext>(options =>
            {
                var connectionString = configuration.GetConnectionString(ConnectionStringName);
                options.UseNpgsql(connectionString);
            });
        return services;
    }

    internal static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseReDoc(reDoc =>
        {
            reDoc.RoutePrefix = "docs";
            reDoc.SpecUrl("/swagger/v1/swagger.json");
            reDoc.DocumentTitle = "Minimal API demo v1";
        });
        return app;
    }

    internal static IEndpointRouteBuilder MapInfrastructure(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGraphQL();
        return endpoints;
    }
}
public class MediatRAssembly { };
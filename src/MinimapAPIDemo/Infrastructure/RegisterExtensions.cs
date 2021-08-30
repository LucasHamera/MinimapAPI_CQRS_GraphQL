using MediatR;
using Microsoft.OpenApi.Models;
using MinimapAPIDemo.Core.Todos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MinimapAPIDemo.Infrastructure;
using MinimapAPIDemo.Application.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimapAPIDemo.Infrastructure.Core.Todos;
using MinimapAPIDemo.Infrastructure.Decorators;

namespace MinimapAPIDemo.Infrastructure;
internal static class RegisterExtensions
{
    private const string ConnectionStringName = "TodoDatabase";

    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddGraphQLServer();

        return services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API demo", Version = "v1" });
            })
            .AddMediatR(typeof(RegisterExtensions))
            .AddDbContext<ApiContext>(options =>
            {
                var connectionString = configuration.GetConnectionString(ConnectionStringName);
                options.UseNpgsql(connectionString);
            })
            .AddTransient<ITodoRepository, TodoRepository>()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior <,>));
    }

    internal static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        return app
            .UseSwagger()
            .UseReDoc(reDoc =>
            {
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl("/swagger/v1/swagger.json");
                reDoc.DocumentTitle = "Minimal API demo v1";
            });
    }

    internal static IEndpointRouteBuilder MapInfrastructure(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGraphQL();
        return endpoints;
    }
}
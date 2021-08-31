using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using MinimapAPIDemo.Infrastructure.GraphQL;
using MinimapAPIDemo.Infrastructure.MediatR;
using MinimapAPIDemo.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using MinimapAPIDemo.Infrastructure.Core.Todos;
using MinimapAPIDemo.Infrastructure.Core.Identity;

namespace MinimapAPIDemo.Infrastructure;

internal static class RegisterExtensions
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddGraphQL()
            .AddMediatR()
            .AddDatabase(configuration)
            .AddTodos()
            .AddIdentity(configuration);


        return services
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API demo", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
    }

    internal static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        return app
            .UseIdentity()
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
        return endpoints
            .MapGraphQLEndpoint();
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MinimapAPIDemo.Infrastructure.GraphQL;

public static class GraphQLRegisterExtensions
{
    internal static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<GraphQLQuery>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }

    internal static IEndpointRouteBuilder MapGraphQLEndpoint(this IEndpointRouteBuilder endpoints)
    {
       endpoints
            .MapGraphQL();
       return endpoints;
    }
}

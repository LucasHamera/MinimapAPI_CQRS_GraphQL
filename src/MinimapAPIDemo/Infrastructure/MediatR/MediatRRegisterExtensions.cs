using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MinimapAPIDemo.Infrastructure.MediatR.Pipelines;

namespace MinimapAPIDemo.Infrastructure.MediatR;

public static class MediatRRegisterExtensions
{
    internal static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
    }
}

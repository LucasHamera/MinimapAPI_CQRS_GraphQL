using MediatR;
using System.Reflection;
using MinimapAPIDemo.Application.Shared;
using MinimapAPIDemo.Application.Shared.Event;
using MinimapAPIDemo.Application.Shared.Query;
using Microsoft.Extensions.DependencyInjection;
using MinimapAPIDemo.Application.Shared.Command;
using MinimapAPIDemo.Infrastructure.MediatR.Pipelines;

namespace MinimapAPIDemo.Infrastructure.MediatR;

public static class MediatRRegisterExtensions
{
    internal static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddTransient<ICommandBus, CommandBus>()
            .AddCommandHandlers()
            .AddTransient<IQueryBus, QueryBus>()
            .AddQueryHandlers()
            .AddTransient<IEventBus, EventBus>()
            .AddEventHandlers()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
    }

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        return services
            .Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
    }

    private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        return services
            .Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
    }

    private static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        return services
            .Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
    }
}

using MediatR;
using System.Reflection;
using MinimapAPIDemo.Application.Shared;
using MinimapAPIDemo.Application.Shared.Event;
using MinimapAPIDemo.Application.Shared.Query;
using Microsoft.Extensions.DependencyInjection;
using MinimapAPIDemo.Application.Shared.Command;
using MinimapAPIDemo.Infrastructure.MediatR.Decorators;

namespace MinimapAPIDemo.Infrastructure.MediatR;

public static class MediatRRegisterExtensions
{
    internal static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddTransient<ICommandBus, CommandBus>()
            .Decorate<ICommandBus, UnitOfWorkCommandBusDecorator>()
            .AddTransient<IQueryBus, QueryBus>()
            .AddTransient<IEventBus, EventBus>();
    }
}

using System;
using System.Threading.Tasks;
using MinimapAPIDemo.Application.Shared.Event;
using Microsoft.Extensions.DependencyInjection;

namespace MinimapAPIDemo.Infrastructure.MediatR;

public class EventBus: IEventBus
{
    private readonly IServiceProvider _serviceProvider;

    public EventBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

        foreach (var handler in handlers)
        {
            await handler.Handle(@event);
        }
    }
}

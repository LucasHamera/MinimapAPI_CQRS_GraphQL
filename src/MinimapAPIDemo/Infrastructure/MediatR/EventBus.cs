using MediatR;
using MinimapAPIDemo.Application.Shared.Event;

namespace MinimapAPIDemo.Infrastructure.MediatR;

public class EventBus: IEventBus
{
    private readonly IMediator _mediator;

    public EventBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        await _mediator.Publish(@event);
    }
}

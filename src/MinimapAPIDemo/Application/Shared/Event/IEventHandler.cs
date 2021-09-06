using MediatR;

namespace MinimapAPIDemo.Application.Shared.Event;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}

using System.Threading.Tasks;

namespace MinimapAPIDemo.Application.Shared.Event;

public interface IEventHandler<in TEvent> where TEvent : IEvent
{
    Task Handle(TEvent @event);
}

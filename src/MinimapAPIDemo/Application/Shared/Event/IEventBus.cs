using System.Threading.Tasks;

namespace MinimapAPIDemo.Application.Shared.Event;

public interface IEventBus
{
    Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
}

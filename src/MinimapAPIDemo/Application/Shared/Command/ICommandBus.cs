using System.Threading;
using System.Threading.Tasks;

namespace MinimapAPIDemo.Application.Shared.Command;

public interface ICommandBus
{
    Task Send<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand;
}

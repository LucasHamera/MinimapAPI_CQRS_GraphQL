using MediatR;
using MinimapAPIDemo.Application.Shared;
using MinimapAPIDemo.Application.Shared.Command;

namespace MinimapAPIDemo.Infrastructure.MediatR;

public class CommandBus: ICommandBus
{
    private readonly IMediator _mediator;

    public CommandBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Unit> Send<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
    {
        return await _mediator.Send(command, cancellationToken);
    }
}

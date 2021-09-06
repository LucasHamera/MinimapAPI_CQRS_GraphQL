using MediatR;
using MinimapAPIDemo.Application.Shared;
using MinimapAPIDemo.Infrastructure.Database;
using MinimapAPIDemo.Application.Shared.Command;

namespace MinimapAPIDemo.Infrastructure.MediatR.Decorators;
public class UnitOfWorkCommandBusDecorator : ICommandBus
{
    private readonly ICommandBus _commandBus;
    private readonly IApiContext _dbContext;

    public UnitOfWorkCommandBusDecorator(ICommandBus commandBus, IApiContext dbContext)
    {
        _commandBus = commandBus;
        _dbContext = dbContext;
    }

    public async Task<Unit> Send<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
    {
        var result = await _commandBus.Send<TCommand>(command, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return result;
    }
}

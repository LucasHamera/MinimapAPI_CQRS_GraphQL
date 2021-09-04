using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.OpenApi.Writers;
using MinimapAPIDemo.Application.Shared;
using Microsoft.Extensions.DependencyInjection;
using MinimapAPIDemo.Application.Shared.Command;

namespace MinimapAPIDemo.Infrastructure.MediatR;

public class CommandBus: ICommandBus
{
    private readonly IServiceProvider _serviceProvider;

    public CommandBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Send<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.Handle(command, cancellationToken);
    }
}

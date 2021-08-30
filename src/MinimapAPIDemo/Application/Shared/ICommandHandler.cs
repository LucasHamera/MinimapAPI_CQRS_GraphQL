using MediatR;

namespace MinimapAPIDemo.Application.Shared;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
{

}
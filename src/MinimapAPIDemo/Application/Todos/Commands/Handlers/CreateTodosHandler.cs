using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MinimapAPIDemo.Core.Todos;
using MinimapAPIDemo.Application.Shared;
using MinimapAPIDemo.Application.Todos.Commands;

namespace MinimapAPIDemo.Application.Todos.Commands.Handlers;

public class CreateTodosHandler : ICommandHandler<CreateTodo>
{
    private readonly ITodoRepository _todoRepository;

    public CreateTodosHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<Unit> Handle(CreateTodo request, CancellationToken cancellationToken)
    {
        var todo = new Todo(request.Id, request.Text, request.IsCompleted);
        await _todoRepository.AddAsync(todo, cancellationToken);
        return Unit.Value;
    }
}
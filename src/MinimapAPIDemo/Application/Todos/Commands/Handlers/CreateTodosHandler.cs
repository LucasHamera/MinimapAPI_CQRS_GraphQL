using System.Threading;
using System.Threading.Tasks;
using MinimapAPIDemo.Core.Todos;
using MinimapAPIDemo.Application.Shared;
using MinimapAPIDemo.Application.Shared.Event;
using MinimapAPIDemo.Application.Todos.Events;
using MinimapAPIDemo.Application.Todos.Commands;
using MediatR;

namespace MinimapAPIDemo.Application.Todos.Commands.Handlers;

public class CreateTodosHandler : ICommandHandler<CreateTodo>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IEventBus _eventBus;

    public CreateTodosHandler(ITodoRepository todoRepository, IEventBus eventBus)
    {
        _todoRepository = todoRepository;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(CreateTodo request, CancellationToken cancellationToken)
    {
        var todo = new Todo(request.Id, request.Text, request.IsCompleted);
        await _todoRepository.AddAsync(todo, cancellationToken);
        await _eventBus.Publish(new TodoCreated(todo.Id, todo.Text, todo.IsCompleted));
        return Unit.Value;
    }
}
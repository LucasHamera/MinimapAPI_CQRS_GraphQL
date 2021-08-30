using System.Threading;
using MinimapAPIDemo.Core;
using System.Threading.Tasks;
using MinimapAPIDemo.Core.Todos;
using MinimapAPIDemo.Infrastructure;

namespace MinimapAPIDemo.Infrastructure.Core.Todos;

public class TodoRepository : ITodoRepository
{
    private readonly IApiContext _dbContext;

    public TodoRepository(IApiContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Todo todo, CancellationToken cancellationToken)
    {
        await _dbContext
            .Todos
            .AddAsync(todo, cancellationToken);
    }
}
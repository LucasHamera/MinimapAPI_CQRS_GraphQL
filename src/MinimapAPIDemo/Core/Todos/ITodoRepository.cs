using System.Threading;
using System.Threading.Tasks;
using MinimapAPIDemo.Core.Todos;

namespace MinimapAPIDemo.Core.Todos;

public interface ITodoRepository
{
    Task AddAsync(Todo todo, CancellationToken cancellationToken);
}
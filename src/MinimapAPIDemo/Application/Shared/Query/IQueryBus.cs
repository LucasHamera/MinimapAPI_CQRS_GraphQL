using System.Threading;
using System.Threading.Tasks;

namespace MinimapAPIDemo.Application.Shared.Query;

public interface IQueryBus
{
    Task<TResult> Send<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery<TResult>;
}

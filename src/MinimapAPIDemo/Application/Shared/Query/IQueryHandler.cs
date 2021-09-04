using System.Threading;
using System.Threading.Tasks;

namespace MinimapAPIDemo.Application.Shared.Query;

public interface IQueryHandler<in TQuery, TResult> where TQuery: IQuery<TResult>
{
    Task<TResult> Handle(TQuery @event, CancellationToken cancellationToken);
}

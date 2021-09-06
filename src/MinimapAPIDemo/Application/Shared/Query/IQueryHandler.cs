using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MinimapAPIDemo.Application.Shared.Query;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery: IQuery<TResult>
{
}

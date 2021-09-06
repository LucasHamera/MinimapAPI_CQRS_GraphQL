using MediatR;
using MinimapAPIDemo.Application.Shared.Query;

namespace MinimapAPIDemo.Infrastructure.MediatR;

public class QueryBus: IQueryBus
{
    private readonly IMediator _mediator;

    public QueryBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TResult> Send<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery<TResult>
    {
        return await _mediator.Send(query, cancellationToken);
    }
}

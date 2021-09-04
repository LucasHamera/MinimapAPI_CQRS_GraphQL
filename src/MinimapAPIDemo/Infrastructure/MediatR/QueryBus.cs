using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MinimapAPIDemo.Application.Shared.Query;

namespace MinimapAPIDemo.Infrastructure.MediatR;

public class QueryBus: IQueryBus
{
    private readonly IServiceProvider _serviceProvider;

    public QueryBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> Send<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery<TResult>
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await handler.Handle(query, cancellationToken);
    }
}

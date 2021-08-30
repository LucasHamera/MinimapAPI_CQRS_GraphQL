using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MinimapAPIDemo.Infrastructure.Decorators;
public class UnitOfWorkPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly TodoContext _dbContext;

    public UnitOfWorkPipelineBehavior(TodoContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var response = await next();
        await _dbContext.SaveChangesAsync(cancellationToken);
        return response;
    }
}

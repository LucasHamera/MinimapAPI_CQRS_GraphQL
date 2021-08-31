using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MinimapAPIDemo.Infrastructure;
using MinimapAPIDemo.Infrastructure.Database;

namespace MinimapAPIDemo.Infrastructure.MediatR.Pipelines
{
    public class UnitOfWorkPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IApiContext _dbContext;

        public UnitOfWorkPipelineBehavior(IApiContext dbContext)
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
}
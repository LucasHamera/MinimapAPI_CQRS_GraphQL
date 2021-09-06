using MediatR;

namespace MinimapAPIDemo.Application.Shared.Query;

public interface IQuery<TResult>: IRequest<TResult>
{
}

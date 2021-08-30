using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using MinimapAPIDemo.Core.Todos;
using MinimapAPIDemo.Core.Identity;

namespace MinimapAPIDemo.Infrastructure.GraphQL;
public class GraphQLQuery
{
    [UseDbContext(typeof(ApiContext))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Todo> Todos([ScopedService] ApiContext dbContext) 
        => dbContext.Todos;

    [UseDbContext(typeof(ApiContext))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> Users([ScopedService] ApiContext dbContext)
        => dbContext.Users;
}

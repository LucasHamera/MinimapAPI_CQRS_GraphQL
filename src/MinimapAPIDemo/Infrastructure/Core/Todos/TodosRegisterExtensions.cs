using MinimapAPIDemo.Core.Todos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MinimapAPIDemo.Infrastructure.Core.Todos;

public static class TodosRegisterExtensions
{
    internal static IServiceCollection AddTodos(this IServiceCollection services)
    {
        return services
            .AddTransient<ITodoRepository, TodoRepository>(); ;
    }
}

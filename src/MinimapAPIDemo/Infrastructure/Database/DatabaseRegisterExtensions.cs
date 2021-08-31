using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MinimapAPIDemo.Infrastructure.Database;

public static class DatabaseRegisterExtensions
{
    private const string ConnectionStringName = "ApiDatabase";

    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContextFactory<ApiContext>(options =>
            {
                var connectionString = configuration.GetConnectionString(ConnectionStringName);
                options.UseNpgsql(connectionString);

                //options.UseInMemoryDatabase(databaseName: "Database");
            })
            .AddScoped<IApiContext, ApiContext>();
    }
}

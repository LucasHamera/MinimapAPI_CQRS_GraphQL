using System.Threading;
using System.Threading.Tasks;
using MinimapAPIDemo.Core.Todos;
using MinimapAPIDemo.Core.Identity;
using Microsoft.EntityFrameworkCore;

namespace MinimapAPIDemo.Infrastructure.Database;

public interface IApiContext
{
    public DbSet<Todo> Todos { get; }
    public DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
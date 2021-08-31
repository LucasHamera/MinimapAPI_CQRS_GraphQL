using MinimapAPIDemo.Core;
using MinimapAPIDemo.Core.Todos;
using MinimapAPIDemo.Core.Identity;
using Microsoft.EntityFrameworkCore;
using MinimapAPIDemo.Infrastructure.Database;

namespace MinimapAPIDemo.Infrastructure.Database;

public class ApiContext : DbContext, IApiContext
{
    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder
            .Entity<User>()
            // admin admin
            .HasData(new User("admin", "AQAAAAEAACcQAAAAELibfyhEQ34pzbtFEsXax3A6gkWiF0sHXeZ+EiaPHcLX9yG7eVjoK3+phXvHIyKJhw=="));

    }
}
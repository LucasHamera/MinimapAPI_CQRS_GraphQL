using MinimapAPIDemo.Core;
using MinimapAPIDemo.Core.Todos;
using MinimapAPIDemo.Core.Identity;
using Microsoft.EntityFrameworkCore;

namespace MinimapAPIDemo.Infrastructure;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}
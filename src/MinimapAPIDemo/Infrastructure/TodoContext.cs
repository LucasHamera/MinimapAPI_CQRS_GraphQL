using MinimapAPIDemo.Core;
using Microsoft.EntityFrameworkCore;

namespace MinimapAPIDemo.Infrastructure;

public class TodoContext: DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; }
}

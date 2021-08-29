using System;

namespace MinimapAPIDemo.Core;

public class Todo
{
    public Todo(Guid id, string text, bool isCompleted)
    {
        Id = id;
        Text = text;
        IsCompleted = isCompleted;
    }

    public Guid Id { get; }

    public string Text { get;  }

    public bool IsCompleted { get; }
}

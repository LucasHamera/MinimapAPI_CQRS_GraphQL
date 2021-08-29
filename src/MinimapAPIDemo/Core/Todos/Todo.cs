using System;

namespace MinimapAPIDemo.Core.Todos;

public class Todo
{
    private Todo()
    {

    }

    public Todo(Guid id, string text, bool isCompleted)
    {
        Id = id;
        Text = text;
        IsCompleted = isCompleted;
    }

    public Guid Id { get; private set; }

    public string Text { get; private set; }

    public bool IsCompleted { get; private set; }
}
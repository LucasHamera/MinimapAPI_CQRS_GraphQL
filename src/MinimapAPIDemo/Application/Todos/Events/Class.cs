using System;

namespace MinimapAPIDemo.Application.Todos.Events;

public record TodoCreated(Guid Id, string Text, bool IsCompleted);

using System;
using MinimapAPIDemo.Application.Shared.Event;

namespace MinimapAPIDemo.Application.Todos.Events;

public record TodoCreated(Guid Id, string Text, bool IsCompleted): IEvent;

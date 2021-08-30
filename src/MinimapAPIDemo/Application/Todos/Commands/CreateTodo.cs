using System;
using MinimapAPIDemo.Application.Shared;

namespace MinimapAPIDemo.Application.Todos.Commands;

public record CreateTodo(Guid Id, string Text, bool IsCompleted) : ICommand;
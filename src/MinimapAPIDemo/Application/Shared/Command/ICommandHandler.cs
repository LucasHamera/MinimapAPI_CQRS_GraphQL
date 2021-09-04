﻿using System.Threading;
using System.Threading.Tasks;

namespace MinimapAPIDemo.Application.Shared;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellationToken);
}
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using MinimapAPIDemo.Application.Todos;
using Microsoft.AspNetCore.Authorization;
using MinimapAPIDemo.Application.Shared.Query;
using MinimapAPIDemo.Application.Identity.DTOs;
using MinimapAPIDemo.Application.Shared.Command;
using MinimapAPIDemo.Application.Todos.Commands;
using MinimapAPIDemo.Application.Identity.Queries;

namespace MinimapAPIDemo;

public static class ApiRegisterExtension
{
    internal static IEndpointRouteBuilder MapApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("api/login",
                [AllowAnonymous]
                [ProducesResponseType(StatusCodes.Status200OK)]
                async (IQueryBus commandBus, GenerateJWT command, CancellationToken cancellationToken) =>
                {
                    var jwtToken = await commandBus.Send<GenerateJWT, JsonWebTokenDTO>(command, cancellationToken);
                    return Results.Ok(jwtToken);
                });

        endpoints
            .MapPost("api/todo",
                [Authorize()]
                [ProducesResponseType(StatusCodes.Status201Created)]
                async (ICommandBus commandBus, CreateTodo command, CancellationToken cancellationToken) =>
                {
                    await commandBus.Send(command, cancellationToken);
                    return Results.Created($"/api/todo/{command.Id}", new { command.Id });
                });

        return endpoints;
    }
}
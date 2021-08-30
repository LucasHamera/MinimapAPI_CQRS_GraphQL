using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using MinimapAPIDemo.Application.Todos;
using MinimapAPIDemo.Application.Todos.Commands;
using MinimapAPIDemo.Application.Identity.Commands;

namespace MinimapAPIDemo;

public static class ApiRegisterExtension
{
    internal static IEndpointRouteBuilder MapApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("api/login",
                [AllowAnonymous]
                [ProducesResponseType(StatusCodes.Status200OK)]
                async (IMediator mediator, LoginUser command) =>
                {
                    var jwtToken = await mediator.Send(command);
                    return Results.Ok(jwtToken);
                });

        endpoints
            .MapPost("api/todo",
                [Authorize()]
                [ProducesResponseType(StatusCodes.Status201Created)]
                async (IMediator mediator, CreateTodo command) =>
                {
                    await mediator.Send(command);
                    return Results.Created($"/api/todo/{command.Id}", new { command.Id });
                });

        return endpoints;
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MinimapAPIDemo.Application.Todos;

namespace MinimapAPIDemo;

public static class ApiRegisterExtension
{
    internal static IEndpointRouteBuilder MapApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("api/todo",
            [ProducesResponseType(StatusCodes.Status201Created)]
            async (IMediator mediator, CreateTodo command) =>
            {
                await mediator.Send(command);
                return Results.Created($"/api/todo/{command.Id}", new { command.Id });
            });

        return endpoints;
    }
}

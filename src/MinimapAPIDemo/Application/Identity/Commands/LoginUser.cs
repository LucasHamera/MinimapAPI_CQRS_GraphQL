using MediatR;
using MinimapAPIDemo.Application.Identity.DTOs;

namespace MinimapAPIDemo.Application.Identity.Commands;

public record LoginUser(string Login, string Password) : IRequest<JsonWebTokenDTO>;
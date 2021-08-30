using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MinimapAPIDemo.Application.Identity.DTOs;
using MinimapAPIDemo.Application.Identity.Commands;
using MinimapAPIDemo.Application.Identity.Services;

namespace MinimapAPIDemo.Application.Identity.Commands.Handlers;
public class LoginUserHandler : IRequestHandler<LoginUser, JsonWebTokenDTO>
{
    private readonly IIdentityService _identityService;

    public LoginUserHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task<JsonWebTokenDTO> Handle(LoginUser request, CancellationToken cancellationToken)
    {
        return _identityService.LoginAsync(request, cancellationToken);
    }
}
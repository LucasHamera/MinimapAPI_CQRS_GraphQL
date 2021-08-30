using System.Threading;
using System.Threading.Tasks;
using MinimapAPIDemo.Application.Identity;
using MinimapAPIDemo.Application.Identity.DTOs;
using MinimapAPIDemo.Application.Identity.Services;
using MinimapAPIDemo.Application.Identity.Commands;

namespace MinimapAPIDemo.Infrastructure.Core.Identity;

public class IdentityService : IIdentityService
{
    public Task<JsonWebTokenDTO> LoginAsync(LoginUser request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}
using System.Threading;
using System.Threading.Tasks;
using MinimapAPIDemo.Application.Identity.DTOs;
using MinimapAPIDemo.Application.Identity.Commands;

namespace MinimapAPIDemo.Application.Identity.Services;

public interface IIdentityService
{
    Task<JsonWebTokenDTO> LoginAsync(LoginUser request, CancellationToken cancellationToken);
}
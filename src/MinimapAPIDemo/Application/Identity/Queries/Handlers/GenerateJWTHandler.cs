using System;
using System.Threading;
using System.Threading.Tasks;
using MinimapAPIDemo.Application.Shared.Query;
using MinimapAPIDemo.Application.Identity.DTOs;
using MinimapAPIDemo.Application.Identity.Services;

namespace MinimapAPIDemo.Application.Identity.Queries.Handlers
{
    public class GenerateJWTHandler : IQueryHandler<GenerateJWT, JsonWebTokenDTO>
    {
        private readonly IIdentityService _identityService;

        public GenerateJWTHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<JsonWebTokenDTO> Handle(GenerateJWT request, CancellationToken cancellationToken)
        {
            return await _identityService.LoginAsync(request, cancellationToken);
        }
    }
}
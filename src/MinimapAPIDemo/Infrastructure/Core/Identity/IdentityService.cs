using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MinimapAPIDemo.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MinimapAPIDemo.Application.Identity;
using MinimapAPIDemo.Application.Identity.DTOs;
using MinimapAPIDemo.Application.Identity.Services;
using MinimapAPIDemo.Application.Identity.Exceptions;
using MinimapAPIDemo.Application.Identity.Queries;
using MinimapAPIDemo.Infrastructure.Database;

namespace MinimapAPIDemo.Infrastructure.Core.Identity;

public class IdentityService : IIdentityService
{
    private readonly IApiContext _dbContext;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher<User> _passwordHasher;

    public IdentityService(IApiContext dbContext, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher<User> passwordHasher)
    {
        _dbContext = dbContext;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<JsonWebTokenDTO> LoginAsync(GenerateJWT request, CancellationToken cancellationToken)
    {
        var user = await _dbContext
            .Users
            .FirstOrDefaultAsync(x => x.Login.Equals(request.Login), cancellationToken);

        if (user is null)
            throw new InvalidCredentialsException();

        if (!ValidatePassword(user, request.Password)) 
            throw new InvalidCredentialsException();

        var token = _jwtTokenGenerator.Generate(user);

        return new JsonWebTokenDTO(user.Login, token);
    }

    private bool ValidatePassword(User user, string providedPassword)
        => _passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword) != PasswordVerificationResult.Failed;
}
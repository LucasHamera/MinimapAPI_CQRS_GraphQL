using System.Text;
using MinimapAPIDemo.Core.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace MinimapAPIDemo.Infrastructure.Core.Identity;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}

public class JwtTokenGenerator: IJwtTokenGenerator
{
    private readonly JwtConfiguration _configuration;

    public JwtTokenGenerator(JwtConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(User user)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_configuration.Key);
        var secret = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration.Issuer,
            claims: null,
            expires: System.DateTime.Now.AddMinutes(120),
            signingCredentials: credentials
        );

        var jwtTokenHandler = new JwtSecurityTokenHandler();
        return jwtTokenHandler.WriteToken(token);
    }
}

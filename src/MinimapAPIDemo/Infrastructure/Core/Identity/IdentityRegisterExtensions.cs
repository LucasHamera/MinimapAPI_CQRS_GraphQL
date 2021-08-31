using System.Text;
using MinimapAPIDemo.Core.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimapAPIDemo.Application.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MinimapAPIDemo.Infrastructure.Core.Identity;

public static class IdentityRegisterExtensions
{
    internal static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = new JwtConfiguration();
        configuration.GetSection("JWT").Bind(jwtConfiguration);

        services
            .AddTransient<IIdentityService, IdentityService>()
            .AddTransient<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddTransient<IJwtTokenGenerator, JwtTokenGenerator>(_ => new JwtTokenGenerator(jwtConfiguration))
            .AddAuthorization()
            .AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key))
                };
            });
        return services;
    }

    internal static IApplicationBuilder UseIdentity(this IApplicationBuilder app)
    {
        return app
            .UseAuthentication()
            .UseAuthorization();
    }
}

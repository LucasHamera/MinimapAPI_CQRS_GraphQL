using MediatR;
using System.Reflection;
using System.Text;
using Microsoft.OpenApi.Models;
using MinimapAPIDemo.Core.Todos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MinimapAPIDemo.Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using MinimapAPIDemo.Infrastructure.Pipelines;
using Microsoft.Extensions.DependencyInjection;
using MinimapAPIDemo.Infrastructure.Core.Todos;
using MinimapAPIDemo.Application.Identity.Services;
using MinimapAPIDemo.Infrastructure.Core.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MinimapAPIDemo.Infrastructure;

internal static class RegisterExtensions
{
    private const string ConnectionStringName = "TodoDatabase";

    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddGraphQLServer();

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
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Key))
                };
            });

        return services
            .AddEndpointsApiExplorer()
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API demo", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            })
            .AddDbContext<IApiContext, ApiContext>(options =>
            {
                var connectionString = configuration.GetConnectionString(ConnectionStringName);
                options.UseNpgsql(connectionString);
            })
            .AddTransient<ITodoRepository, TodoRepository>()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
    }

    internal static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        return app
            .UseAuthentication()
            .UseAuthorization()
            .UseSwagger()
            .UseReDoc(reDoc =>
            {
                reDoc.RoutePrefix = "docs";
                reDoc.SpecUrl("/swagger/v1/swagger.json");
                reDoc.DocumentTitle = "Minimal API demo v1";
            });
    }

    internal static IEndpointRouteBuilder MapInfrastructure(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGraphQL();
        return endpoints;
    }
}
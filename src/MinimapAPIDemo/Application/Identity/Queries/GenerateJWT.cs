using MinimapAPIDemo.Application.Shared.Query;
using MinimapAPIDemo.Application.Identity.DTOs;

namespace MinimapAPIDemo.Application.Identity.Queries;

public record GenerateJWT(string Login, string Password) : IQuery<JsonWebTokenDTO>;
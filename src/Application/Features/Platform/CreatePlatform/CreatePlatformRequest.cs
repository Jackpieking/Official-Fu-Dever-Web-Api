using Application.Features.Platform.CreatePlatform.Middlewares;
using MediatR;

namespace Application.Features.Platform.CreatePlatform;

/// <summary>
///     Create platform request.
/// </summary>
public sealed class CreatePlatformRequest :
    IRequest<CreatePlatformResponse>,
    ICreatePlatformMiddleware
{
    public string NewPlatformName { get; init; }
}

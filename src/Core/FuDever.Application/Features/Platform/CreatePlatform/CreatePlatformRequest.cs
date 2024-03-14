using FuDever.Application.Features.Platform.CreatePlatform.Middlewares;
using MediatR;

namespace FuDever.Application.Features.Platform.CreatePlatform;

/// <summary>
///     Create platform request.
/// </summary>
public sealed class CreatePlatformRequest :
    IRequest<CreatePlatformResponse>,
    ICreatePlatformMiddleware
{
    public string NewPlatformName { get; init; }
}

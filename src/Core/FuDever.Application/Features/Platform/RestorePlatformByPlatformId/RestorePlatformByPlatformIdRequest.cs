using FuDever.Application.Features.Platform.RestorePlatformByPlatformId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Platform.RestorePlatformByPlatformId;

/// <summary>
///     Restore platform by platform id request.
/// </summary>
public sealed class RestorePlatformByPlatformIdRequest :
    IRequest<RestorePlatformByPlatformIdResponse>,
    IRestorePlatformByPlatformIdMiddleware
{
    public Guid PlatformId { get; init; }
}

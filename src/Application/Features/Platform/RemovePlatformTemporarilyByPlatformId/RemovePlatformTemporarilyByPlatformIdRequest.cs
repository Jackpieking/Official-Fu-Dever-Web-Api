using Application.Features.Platform.RemovePlatformTemporarilyByPlatformId.Middlewares;
using MediatR;
using System;

namespace Application.Features.Platform.RemovePlatformTemporarilyByPlatformId;

/// <summary>
///     Remove platform temporarily by platfrom id request.
/// </summary>
public sealed class RemovePlatformTemporarilyByPlatformIdRequest :
    IRequest<RemovePlatformTemporarilyByPlatformIdResponse>,
    IRemovePlatformTemporarilyByPlatformIdMiddleware
{
    public Guid PlatformId { get; init; }

    public Guid PlatformRemovedBy { get; init; }
}

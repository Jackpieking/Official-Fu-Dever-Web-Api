using Application.Features.Platform.UpdatePlatformByPlatformId.Middlewares;
using MediatR;
using System;

namespace Application.Features.Platform.UpdatePlatformByPlatformId;

/// <summary>
///     Update platform request.
/// </summary>
public sealed class UpdatePlatformByPlatformIdRequest :
    IRequest<UpdatePlatformByPlatformIdResponse>,
    IUpdatePlatformByPlatformIdMiddleware
{
    public Guid PlatformId { get; init; }

    public string NewPlatformName { get; init; }

    public Guid PlatformUpdatedBy { get; init; }
}

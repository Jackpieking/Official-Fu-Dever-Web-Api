using Application.Features.Position.RestorePositionByPositionId.Middlewares;
using MediatR;
using System;

namespace Application.Features.Position.RestorePositionByPositionId;

/// <summary>
///     Restore position by position id request.
/// </summary>
public sealed class RestorePositionByPositionIdRequest :
    IRequest<RestorePositionByPositionIdResponse>,
    IRestorePositionByPositionIdMiddleware
{
    public Guid PositionId { get; init; }
}

using Application.Features.Position.UpdatePosition.Middlewares;
using MediatR;
using System;

namespace Application.Features.Position.UpdatePosition;

/// <summary>
///     Update position by position id request.
/// </summary>
public sealed class UpdatePositionByPositionIdRequest :
    IRequest<UpdatePositionByPositionIdResponse>,
    IUpdatePositionByPositionIdMiddleware
{
    public Guid PositionId { get; init; }

    public string NewPositionName { get; init; }

    public Guid PositionUpdatedBy { get; init; }
}

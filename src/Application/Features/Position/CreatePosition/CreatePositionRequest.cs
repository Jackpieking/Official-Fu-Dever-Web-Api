using Application.Features.Position.CreatePosition.Middlewares;
using MediatR;

namespace Application.Features.Position.CreatePosition;

/// <summary>
///     Create position request.
/// </summary>
public sealed class CreatePositionRequest :
    IRequest<CreatePositionResponse>,
    ICreatePositionMiddleware
{
    public string NewPositionName { get; init; }
}

using Application.Features.Position.GetAllPositionsByPositionName.Middlewares;
using MediatR;

namespace Application.Features.Position.GetAllPositionsByPositionName;

/// <summary>
///     Get all position by position name request.
/// </summary>
public sealed class GetAllPositionsByPositionNameRequest :
    IRequest<GetAllPositionsByPositionNameResponse>,
    IGetAllPositionsByPositionNameMiddleware
{
    public string PositionName { get; init; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; } = default;
}

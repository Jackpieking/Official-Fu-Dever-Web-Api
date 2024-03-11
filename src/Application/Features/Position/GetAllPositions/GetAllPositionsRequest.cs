using Application.Features.Position.GetAllPositions.Middlewares;
using MediatR;

namespace Application.Features.Position.GetAllPositions;

/// <summary>
///     Get all position request.
/// </summary>
public sealed class GetAllPositionsRequest :
    IRequest<GetAllPositionsResponse>,
    IGetAllPositionsMiddleware
{
    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; }
}

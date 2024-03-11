using Application.Features.Platform.GetAllTemporarilyRemovedPlatforms.Middlewares;
using MediatR;

namespace Application.Features.Platform.GetAllTemporarilyRemovedPlatforms;

/// <summary>
///     Get all temporarily removed platforms request.
/// </summary>
public sealed class GetAllTemporarilyRemovedPlatformsRequest :
    IRequest<GetAllTemporarilyRemovedPlatformsResponse>,
    IGetAllTemporarilyRemovedPlatformsMiddleware
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

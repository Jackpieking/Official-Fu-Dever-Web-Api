using FuDever.Application.Features.Platform.GetAllPlatforms.Middlewares;
using MediatR;

namespace FuDever.Application.Features.Platform.GetAllPlatforms;

/// <summary>
///     Get all platforms request
/// </summary>
public sealed class GetAllPlatformsRequest :
    IRequest<GetAllPlatformsResponse>,
    IGetAllPlatformsMiddleware
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

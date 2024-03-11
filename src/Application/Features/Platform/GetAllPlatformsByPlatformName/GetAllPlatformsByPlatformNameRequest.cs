using Application.Features.Platform.GetAllPlatformsByPlatformName.Middlewares;
using MediatR;

namespace Application.Features.Platform.GetAllPlatformsByPlatformName;

/// <summary>
///     Get all platforms by name request.
/// </summary>
public sealed class GetAllPlatformsByPlatformNameRequest :
    IRequest<GetAllPlatformsByPlatformNameResponse>,
    IGetAllPlatformsByPlatformNameMiddleware
{
    public string PlatformName { get; init; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; }
}

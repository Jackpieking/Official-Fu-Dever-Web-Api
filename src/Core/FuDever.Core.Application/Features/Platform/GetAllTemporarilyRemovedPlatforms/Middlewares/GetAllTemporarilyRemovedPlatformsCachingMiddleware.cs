using FuDever.Application.Interfaces.Caching;
using FuDever.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Platform.GetAllTemporarilyRemovedPlatforms.Middlewares;

/// <summary>
///     Get all temporarily removed platforms
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class GetAllTemporarilyRemovedPlatformsCachingMiddleware :
    IPipelineBehavior<
        GetAllTemporarilyRemovedPlatformsRequest,
        GetAllTemporarilyRemovedPlatformsResponse>,
    IGetAllTemporarilyRemovedPlatformsMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllTemporarilyRemovedPlatformsCachingMiddleware(ICacheHandler cacheHandler)
    {
        _cacheHandler = cacheHandler;
    }

    /// <summary>
    ///     Entry to middleware handler.
    ///     Caching the return value.
    ///         If cache expired time is not set, it will not be cached.
    ///         If cache expired time is set or cache does not exist,
    ///             it will be cached.
    ///         If cache exists, it will be returned.
    /// </summary>
    /// <param name="request">
    ///     Current request object.
    /// </param>
    /// <param name="next">
    ///     Navigate to next middleware and get back response.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Response of use case.
    /// </returns>
    public async Task<GetAllTemporarilyRemovedPlatformsResponse> Handle(
        GetAllTemporarilyRemovedPlatformsRequest request,
        RequestHandlerDelegate<GetAllTemporarilyRemovedPlatformsResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        const string cachedKey = nameof(GetAllTemporarilyRemovedPlatformsRequest);

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<GetAllTemporarilyRemovedPlatformsResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<GetAllTemporarilyRemovedPlatformsResponse>.NotFound))
        {
            return cacheModel.Value;
        }

        var response = await next();

        // Caching the return value.
        await _cacheHandler.SetAsync(
            key: cachedKey,
            value: response,
            new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(
                    seconds: request.CacheExpiredTime)
            },
            cancellationToken: cancellationToken);

        return response;
    }
}

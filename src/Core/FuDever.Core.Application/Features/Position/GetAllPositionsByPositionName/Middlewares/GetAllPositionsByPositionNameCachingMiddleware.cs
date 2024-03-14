using FuDever.Application.Interfaces.Caching;
using FuDever.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Position.GetAllPositionsByPositionName.Middlewares;

/// <summary>
///     Get all positions by position name
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class GetAllPositionsByPositionNameCachingMiddleware :
    IPipelineBehavior<
        GetAllPositionsByPositionNameRequest,
        GetAllPositionsByPositionNameResponse>,
    IGetAllPositionsByPositionNameMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllPositionsByPositionNameCachingMiddleware(ICacheHandler cacheHandler)
    {
        _cacheHandler = cacheHandler;
    }

    /// <summary>
    ///     Entry to middleware handler.
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

    public async Task<GetAllPositionsByPositionNameResponse> Handle(
        GetAllPositionsByPositionNameRequest request,
        RequestHandlerDelegate<GetAllPositionsByPositionNameResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        var cachedKey = $"{nameof(GetAllPositionsByPositionNameRequest)}_param_{request.PositionName.ToLower()}";

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<GetAllPositionsByPositionNameResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<GetAllPositionsByPositionNameResponse>.NotFound))
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

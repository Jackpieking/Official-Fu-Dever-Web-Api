using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Caching;
using Application.Models;
using System;

namespace Application.Features.Major.GetAllMajors.Middlewares;

/// <summary>
///     Get all majors request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class GetAllMajorsCachingMiddleware :
    IPipelineBehavior<
        GetAllMajorsRequest,
        GetAllMajorsResponse>,
    IGetAllMajorsMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllMajorsCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<GetAllMajorsResponse> Handle(
        GetAllMajorsRequest request,
        RequestHandlerDelegate<GetAllMajorsResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        const string cachedKey = nameof(GetAllMajorsRequest);

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<GetAllMajorsResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<GetAllMajorsResponse>.NotFound))
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

using Application.Interfaces.Caching;
using Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Major.GetAllMajorsByMajorName.Middlewares;

/// <summary>
///     Get all majors by major name
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class GetAllMajorsByMajorNameCachingMiddleware :
    IPipelineBehavior<
        GetAllMajorsByMajorNameRequest,
        GetAllMajorsByMajorNameResponse>,
    IGetAllMajorsByMajorNameMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllMajorsByMajorNameCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<GetAllMajorsByMajorNameResponse> Handle(
        GetAllMajorsByMajorNameRequest request,
        RequestHandlerDelegate<GetAllMajorsByMajorNameResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        var cachedKey = $"{nameof(GetAllMajorsByMajorNameRequest)}_param_{request.MajorName.ToLower()}";

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<GetAllMajorsByMajorNameResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<GetAllMajorsByMajorNameResponse>.NotFound))
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

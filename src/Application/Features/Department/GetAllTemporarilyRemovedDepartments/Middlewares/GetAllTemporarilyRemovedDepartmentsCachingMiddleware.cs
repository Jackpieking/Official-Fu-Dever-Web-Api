using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Caching;
using Application.Models;
using MediatR;

namespace Application.Features.Department.GetAllTemporarilyRemovedDepartments.Middlewares;

/// <summary>
///     Get all temporarily removed department
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
public class GetAllTemporarilyRemovedDepartmentsCachingMiddleware :
    IPipelineBehavior<
        GetAllTemporarilyRemovedDepartmentsRequest,
        GetAllTemporarilyRemovedDepartmentsResponse>,
    IGetAllTemporarilyRemovedDepartmentsMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllTemporarilyRemovedDepartmentsCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<GetAllTemporarilyRemovedDepartmentsResponse> Handle(
        GetAllTemporarilyRemovedDepartmentsRequest request,
        RequestHandlerDelegate<GetAllTemporarilyRemovedDepartmentsResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        var cachedKey = nameof(GetAllTemporarilyRemovedDepartments);

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<GetAllTemporarilyRemovedDepartmentsResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<GetAllTemporarilyRemovedDepartmentsResponse>.NotFound))
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

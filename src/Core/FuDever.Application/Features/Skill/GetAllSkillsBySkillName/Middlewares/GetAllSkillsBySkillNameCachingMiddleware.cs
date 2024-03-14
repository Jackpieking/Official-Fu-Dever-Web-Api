using FuDever.Application.Interfaces.Caching;
using FuDever.Application.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Features.Skill.GetAllSkillsBySkillName.Middlewares;

/// <summary>
///     Get all skills by skill name
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class GetAllSkillsBySkillNameCachingMiddleware :
    IPipelineBehavior<
        GetAllSkillsBySkillNameRequest,
        GetAllSkillsBySkillNameResponse>,
    IGetAllSkillsBySkillNameMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllSkillsBySkillNameCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<GetAllSkillsBySkillNameResponse> Handle(
        GetAllSkillsBySkillNameRequest request,
        RequestHandlerDelegate<GetAllSkillsBySkillNameResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        var cachedKey = $"{nameof(GetAllSkillsBySkillNameRequest)}_param_{request.SkillName.ToLower()}";

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<GetAllSkillsBySkillNameResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<GetAllSkillsBySkillNameResponse>.NotFound))
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

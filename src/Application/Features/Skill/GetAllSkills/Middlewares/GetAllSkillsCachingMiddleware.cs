using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Caching;
using Application.Models;
using MediatR;

namespace Application.Features.Skill.GetAllSkills.Middlewares;

/// <summary>
///     Get all skills request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class GetAllSkillCachingMiddleware :
    IPipelineBehavior<
        GetAllSkillsRequest,
        GetAllSkillsResponse>,
    IGetAllSkillsMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public GetAllSkillCachingMiddleware(ICacheHandler cacheHandler)
    {
        _cacheHandler = cacheHandler;
    }

    public async Task<GetAllSkillsResponse> Handle(
        GetAllSkillsRequest request,
        RequestHandlerDelegate<GetAllSkillsResponse> next,
        CancellationToken cancellationToken)
    {
        // Cache is not enable.
        if (request.CacheExpiredTime == default)
        {
            return await next();
        }

        var cachedKey = nameof(GetAllSkills);

        // Retrieve from cache.
        var cacheModel = await _cacheHandler.GetAsync<GetAllSkillsResponse>(
            key: cachedKey,
            cancellationToken: cancellationToken);

        // Cache value does not exist.
        if (!Equals(
                objA: cacheModel,
                objB: CacheModel<GetAllSkillsResponse>.NotFound))
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

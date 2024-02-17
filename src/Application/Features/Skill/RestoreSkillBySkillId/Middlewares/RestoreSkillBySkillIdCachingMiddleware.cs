using Application.Interfaces.Caching;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.RestoreSkillBySkillId.Middlewares;

/// <summary>
///     Restore skill by skill
///     id request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RestoreSkillBySkillIdCachingMiddleware :
    IPipelineBehavior<
        RestoreSkillBySkillIdRequest,
        RestoreSkillBySkillIdResponse>,
    IRestoreSkillBySkillIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public RestoreSkillBySkillIdCachingMiddleware(ICacheHandler cacheHandler)
    {
        _cacheHandler = cacheHandler;
    }

    public async Task<RestoreSkillBySkillIdResponse> Handle(
        RestoreSkillBySkillIdRequest request,
        RequestHandlerDelegate<RestoreSkillBySkillIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RestoreSkillBySkillIdStatusCode.OPERATION_SUCCESS)
        {
            await _cacheHandler.RemoveAsync(
                key: nameof(GetAllTemporarilyRemovedSkills),
                cancellationToken: cancellationToken);
        }

        return response;
    }
}

using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Caching;
using MediatR;

namespace Application.Features.Skill.RemoveSkillPermanentlyBySkillId.Middlewares;

/// <summary>
///     Remove skill permanently by skill id
///     request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class RemoveSkillPermanentlyBySkillIdCachingMiddleware :
    IPipelineBehavior<
        RemoveSkillPermanentlyBySkillIdRequest,
        RemoveSkillPermanentlyBySkillIdResponse>,
    IRemoveSkillPermanentlyBySkillIdMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public RemoveSkillPermanentlyBySkillIdCachingMiddleware(ICacheHandler cacheHandler)
    {
        _cacheHandler = cacheHandler;
    }

    public async Task<RemoveSkillPermanentlyBySkillIdResponse> Handle(
        RemoveSkillPermanentlyBySkillIdRequest request,
        RequestHandlerDelegate<RemoveSkillPermanentlyBySkillIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        await _cacheHandler.RemoveAsync(
            key: nameof(GetAllTemporarilyRemovedSkills),
            cancellationToken: cancellationToken);

        return response;
    }
}

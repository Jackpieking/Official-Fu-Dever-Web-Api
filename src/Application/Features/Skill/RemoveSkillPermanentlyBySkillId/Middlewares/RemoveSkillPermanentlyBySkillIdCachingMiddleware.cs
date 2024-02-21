using Application.Features.Skill.GetAllTemporarilyRemovedSkills;
using Application.Interfaces.Caching;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
    public async Task<RemoveSkillPermanentlyBySkillIdResponse> Handle(
        RemoveSkillPermanentlyBySkillIdRequest request,
        RequestHandlerDelegate<RemoveSkillPermanentlyBySkillIdResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == RemoveSkillPermanentlyBySkillIdStatusCode.OPERATION_SUCCESS)
        {
            await _cacheHandler.RemoveAsync(
                key: nameof(GetAllTemporarilyRemovedSkillsRequest),
                cancellationToken: cancellationToken);
        }

        return response;
    }
}

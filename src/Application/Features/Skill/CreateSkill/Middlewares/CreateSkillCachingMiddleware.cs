using Application.Features.Skill.GetAllSkills;
using Application.Features.Skill.GetAllSkillsBySkillName;
using Application.Interfaces.Caching;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Skill.CreateSkill.Middlewares;

/// <summary>
///     Create skill request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2nd
/// </remarks>
internal sealed class CreateSkillCachingMiddleware :
    IPipelineBehavior<
        CreateSkillRequest,
        CreateSkillResponse>,
    ICreateSkillMiddleware
{
    private readonly ICacheHandler _cacheHandler;

    public CreateSkillCachingMiddleware(ICacheHandler cacheHandler)
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
    public async Task<CreateSkillResponse> Handle(
        CreateSkillRequest request,
        RequestHandlerDelegate<CreateSkillResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (response.StatusCode == CreateSkillStatusCode.OPERATION_SUCCESS)
        {
            await Task.WhenAll(
                _cacheHandler.RemoveAsync(
                    key: $"{nameof(GetAllSkillsBySkillNameRequest)}_param_{request.NewSkillName.ToLower()}",
                    cancellationToken: cancellationToken),
                _cacheHandler.RemoveAsync(
                    key: nameof(GetAllSkillsRequest),
                    cancellationToken: cancellationToken));
        }

        return response;
    }
}

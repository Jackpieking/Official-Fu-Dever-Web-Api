using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Caching;
using MediatR;

namespace Application.Features.Skill.CreateSkill.Middlewares;

/// <summary>
///     Create skill request caching middleware.
/// </summary>
/// <remarks>
///     Order: 2st
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

    public async Task<CreateSkillResponse> Handle(
        CreateSkillRequest request,
        RequestHandlerDelegate<CreateSkillResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        await Task.WhenAll(
            _cacheHandler.RemoveAsync(
                key: $"{nameof(GetAllSkillsByName)}_param_{request.NewSkillName.ToLower()}",
                cancellationToken: cancellationToken),
            _cacheHandler.RemoveAsync(
                key: nameof(GetAllSkills),
                cancellationToken: cancellationToken));

        return response;
    }
}

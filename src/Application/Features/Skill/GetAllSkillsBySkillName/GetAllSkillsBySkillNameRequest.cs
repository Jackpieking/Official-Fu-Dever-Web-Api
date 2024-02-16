using Application.Features.Skill.GetAllSkillsByName.Middlewares;
using MediatR;

namespace Application.Features.Skill.GetAllSkillsBySkillName;

/// <summary>
///     Get all skills by name request.
/// </summary>
public sealed class GetAllSkillsBySkillNameRequest :
    IRequest<GetAllSkillsBySkillNameResponse>,
    IGetAllSkillsBySkillNameMiddleware
{
    public string SkillName { get; init; }

    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; } = default;
}
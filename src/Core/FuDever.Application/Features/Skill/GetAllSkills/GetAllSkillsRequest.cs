using FuDever.Application.Features.Skill.GetAllSkills.Middlewares;
using MediatR;

namespace FuDever.Application.Features.Skill.GetAllSkills;

/// <summary>
///     Get all skills request.
/// </summary>
public sealed class GetAllSkillsRequest :
    IRequest<GetAllSkillsResponse>,
    IGetAllSkillsMiddleware
{
    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; }
}

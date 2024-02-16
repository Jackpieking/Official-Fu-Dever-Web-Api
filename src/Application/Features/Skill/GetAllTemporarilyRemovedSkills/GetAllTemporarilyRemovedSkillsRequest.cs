using Application.Features.Skill.GetAllTemporarilyRemovedSkills.Middlewares;
using MediatR;

namespace Application.Features.Skill.GetAllTemporarilyRemovedSkills;

/// <summary>
///     Get all temporarily removed skills request.
/// </summary>
public sealed class GetAllTemporarilyRemovedSkillsRequest :
    IRequest<GetAllTemporarilyRemovedSkillsResponse>,
    IGetAllTemporarilyRemovedSkillsMiddleware
{
    /// <summary>
    ///     How long the result should live in cache.
    /// </summary>
    /// <remarks>
    ///     Don't give this property a value when don't
    ///     want the result to be cached.
    /// </remarks>
    public int CacheExpiredTime { get; init; } = default;
}

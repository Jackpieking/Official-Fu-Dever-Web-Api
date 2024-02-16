using Application.Features.Skill.CreateSkill.Middlewares;
using MediatR;

namespace Application.Features.Skill.CreateSkill;

/// <summary>
///     Create skill request.
/// </summary>
public sealed class CreateSkillRequest :
    IRequest<CreateSkillResponse>,
    ICreateSkillMiddleware
{
    public string NewSkillName { get; init; }
}

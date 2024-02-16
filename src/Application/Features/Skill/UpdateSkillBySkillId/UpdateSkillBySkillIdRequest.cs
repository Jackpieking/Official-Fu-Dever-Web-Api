using System;
using Application.Features.Skill.UpdateSkillBySkillId.Middlewares;
using MediatR;

namespace Application.Features.Skill.UpdateSkillBySkillId;

/// <summary>
///     Update skill by skill id request.
/// </summary>
public sealed class UpdateSkillBySkillIdRequest :
    IRequest<UpdateSkillBySkillIdResponse>,
    IUpdateSkillBySkillIdMiddleware
{
    public Guid SkillId { get; init; }

    public string NewSkillName { get; init; }

    public Guid SkillUpdatedBy { get; init; }
}

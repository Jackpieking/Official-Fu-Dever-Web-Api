using FuDever.Application.Features.Skill.RestoreSkillBySkillId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Skill.RestoreSkillBySkillId;

/// <summary>
///     Restore skill by skill id request.
/// </summary>
public sealed class RestoreSkillBySkillIdRequest :
    IRequest<RestoreSkillBySkillIdResponse>,
    IRestoreSkillBySkillIdMiddleware
{
    public Guid SkillId { get; init; }
}

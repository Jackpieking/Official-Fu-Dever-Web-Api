using System;
using Application.Features.Skill.RemoveSkillPermanentlyBySkillId.Middlewares;
using MediatR;

namespace Application.Features.Skill.RemoveSkillPermanentlyBySkillId;

/// <summary>
///     Remove skill permanently by skill id request.
/// </summary>
public sealed class RemoveSkillPermanentlyBySkillIdRequest :
    IRequest<RemoveSkillPermanentlyBySkillIdResponse>,
    IRemoveSkillPermanentlyBySkillIdMiddleware
{
    public Guid SkillId { get; init; }

    public Guid SkillRemovedBy { get; init; }
}

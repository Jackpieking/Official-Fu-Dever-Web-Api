using FuDever.Application.Features.Skill.RemoveSkillPermanentlyBySkillId.Middlewares;
using MediatR;
using System;

namespace FuDever.Application.Features.Skill.RemoveSkillPermanentlyBySkillId;

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

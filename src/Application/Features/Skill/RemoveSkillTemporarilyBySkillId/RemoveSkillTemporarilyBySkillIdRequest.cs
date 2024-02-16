using System;
using Application.Features.Skill.RemoveSkillTemporarilyBySkillId.Middlewares;
using MediatR;

namespace Application.Features.Skill.RemoveSkillTemporarilyBySkillId;

/// <summary>
///     Remove skill temporarily by skill id request.
/// </summary>
public sealed class RemoveSkillTemporarilyBySkillIdRequest :
    IRequest<RemoveSkillTemporarilyBySkillIdResponse>,
    IRemoveSkillTemporarilyBySkillIdMiddleware
{
    public Guid SkillId { get; init; }

    public Guid SkillRemovedBy { get; init; }
}

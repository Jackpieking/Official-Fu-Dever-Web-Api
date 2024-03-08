namespace Application.Features.Skill.RemoveSkillTemporarilyBySkillId;

/// <summary>
///     Remove skill temporarily by skill id response status code.
/// </summary>
public enum RemoveSkillTemporarilyBySkillIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
    SKILL_IS_NOT_FOUND
}

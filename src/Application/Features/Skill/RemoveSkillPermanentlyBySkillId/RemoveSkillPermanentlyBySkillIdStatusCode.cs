namespace Application.Features.Skill.RemoveSkillPermanentlyBySkillId;

/// <summary>
///     Remove skill permanently by skill id response status code.
/// </summary>
public enum RemoveSkillPermanentlyBySkillIdStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    SKILL_IS_NOT_FOUND,
    SKILL_IS_NOT_TEMPORARILY_REMOVED
}

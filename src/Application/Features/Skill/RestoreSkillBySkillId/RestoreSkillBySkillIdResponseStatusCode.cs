namespace Application.Features.Skill.RestoreSkillBySkillId;

/// <summary>
///     Restore skill by skill id response status code.
/// </summary>
public enum RestoreSkillBySkillIdResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    SKILL_IS_NOT_TEMPORARILY_REMOVED,
    SKILL_IS_NOT_FOUND
}

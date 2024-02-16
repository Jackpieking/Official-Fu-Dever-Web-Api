namespace Application.Features.Skill.UpdateSkillBySkillId;

/// <summary>
///     Update skill by skill id response status code.
/// </summary>
public enum UpdateSkillBySkillIdStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    SKILL_IS_NOT_FOUND,
    SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
    SKILL_ALREADY_EXISTS
}

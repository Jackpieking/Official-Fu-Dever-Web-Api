namespace Application.Features.Skill.CreateSkill;

/// <summary>
///     Create skill response status code.
/// </summary>
public enum CreateSkillResponseStatusCode
{
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
    SKILL_ALREADY_EXISTS
}

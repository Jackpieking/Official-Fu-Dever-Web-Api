using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserSkill;

namespace Persistence.SqlServer2016.Specifications.Entities.UserSkill;

/// <summary>
///     Represent implementation of select fields from the "UserSkills"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromUserSkillSpecification :
    BaseSpecification<Domain.Entities.UserSkill>,
    ISelectFieldsFromUserSkillSpecification
{
    public ISelectFieldsFromUserSkillSpecification Ver1()
    {
        SelectExpression = userSkill => new()
        {
            SkillId = userSkill.SkillId,
            Skill = new(userSkill.Skill.Name)
        };

        return this;
    }

    public ISelectFieldsFromUserSkillSpecification Ver2()
    {
        SelectExpression = userSkill => new()
        {
            UserId = userSkill.UserId
        };

        return this;
    }
}

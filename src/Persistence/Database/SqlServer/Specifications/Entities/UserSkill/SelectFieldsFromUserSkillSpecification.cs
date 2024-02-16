using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserSkill;

namespace Persistence.Database.SqlServer.Specifications.Entities.UserSkill;

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
        SelectExpression = userSkill => Domain.Entities.UserSkill.InitVer2(
            userSkill.SkillId,
            Domain.Entities.Skill.InitVer3(userSkill.Skill.Name));

        return this;
    }

    public ISelectFieldsFromUserSkillSpecification Ver2()
    {
        SelectExpression = userSkill => Domain.Entities.UserSkill.InitVer3(userSkill.UserId);

        return this;
    }
}

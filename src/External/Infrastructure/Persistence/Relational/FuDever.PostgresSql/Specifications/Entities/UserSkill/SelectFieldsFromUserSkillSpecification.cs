using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserSkill;

namespace FuDever.PostgresSql.Specifications.Entities.UserSkill;

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
        SelectExpression = userSkill => Domain.Entities.UserSkill.InitFromDatabaseVer2(
            userSkill.SkillId,
            Domain.Entities.Skill.InitFromDatabaseVer3(userSkill.Skill.Name));

        return this;
    }

    public ISelectFieldsFromUserSkillSpecification Ver2()
    {
        SelectExpression = userSkill => Domain.Entities.UserSkill.InitFromDatabaseVer3(
            userSkill.UserId);

        return this;
    }
}

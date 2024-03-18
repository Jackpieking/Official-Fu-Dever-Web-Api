using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Skill;

namespace FuDever.PostgresSql.Specifications.Entities.Skill;

/// <summary>
///     Represent implementation of select fields from the "Skills"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromSkillSpecification :
    BaseSpecification<Domain.Entities.Skill>,
    ISelectFieldsFromSkillSpecification
{
    public ISelectFieldsFromSkillSpecification Ver1()
    {
        SelectExpression = skill => Domain.Entities.Skill.InitFromDatabaseVer1(
            skill.Id,
            skill.Name);

        return this;
    }

    public ISelectFieldsFromSkillSpecification Ver2()
    {
        SelectExpression = skill => Domain.Entities.Skill.InitFromDatabaseVer4(
            skill.Id,
            skill.Name,
            skill.RemovedAt,
            skill.RemovedBy);

        return this;
    }

    public ISelectFieldsFromSkillSpecification Ver3()
    {
        SelectExpression = skill => Domain.Entities.Skill.InitFromDatabaseVer2(skill.Id);

        return this;
    }

    public ISelectFieldsFromSkillSpecification Ver4()
    {
        SelectExpression = skill => Domain.Entities.Skill.InitFromDatabaseVer3(skill.Name);

        return this;
    }
}

using Domain.Specifications.Base;
using Domain.Specifications.Entities.Skill;

namespace Persistence.Database.SqlServer.Specifications.Entities.Skill;

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
        SelectExpression = skill => Domain.Entities.Skill.Init(
            skill.Id,
            skill.Name);

        return this;
    }

    public ISelectFieldsFromSkillSpecification Ver2()
    {
        SelectExpression = skill => Domain.Entities.Skill.Init(
            skill.Id,
            skill.Name,
            skill.RemovedAt,
            skill.RemovedBy);

        return this;
    }

    public ISelectFieldsFromSkillSpecification Ver3()
    {
        SelectExpression = skill => Domain.Entities.Skill.Init(skill.Id);

        return this;
    }
}

using Domain.Specifications.Base;
using Domain.Specifications.Entities.Skill;

namespace Persistence.SqlServer2016.Specifications.Entities.Skill;

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
        SelectExpression = skill => new()
        {
            Id = skill.Id,
            Name = skill.Name
        };

        return this;
    }

    public ISelectFieldsFromSkillSpecification Ver2()
    {
        SelectExpression = skill => new()
        {
            Id = skill.Id,
            Name = skill.Name,
            RemovedAt = skill.RemovedAt,
            RemovedBy = skill.RemovedBy
        };

        return this;
    }

    public ISelectFieldsFromSkillSpecification Ver3()
    {
        SelectExpression = skill => new()
        {
            Id = skill.Id
        };

        return this;
    }
}

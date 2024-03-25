using FuDever.Domain.EntityBuilders.Skill;
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
        SkillForDatabaseSeedingBuilder builder = new();

        SelectExpression = skill => builder
            .WithId(skill.Id)
            .WithName(skill.Name)
            .Complete();

        return this;
    }

    public ISelectFieldsFromSkillSpecification Ver2()
    {
        SkillForDatabaseSeedingBuilder builder = new();

        SelectExpression = skill => builder
            .WithId(skill.Id)
            .WithName(skill.Name)
            .WithRemovedAt(skill.RemovedAt)
            .WithRemovedBy(skill.RemovedBy)
            .Complete();

        return this;
    }

    public ISelectFieldsFromSkillSpecification Ver3()
    {
        SkillForDatabaseSeedingBuilder builder = new();

        SelectExpression = skill => builder
            .WithId(skill.Id)
            .Complete();

        return this;
    }

    public ISelectFieldsFromSkillSpecification Ver4()
    {
        SkillForDatabaseSeedingBuilder builder = new();

        SelectExpression = skill => builder
            .WithName(skill.Name)
            .Complete();

        return this;
    }
}

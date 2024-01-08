using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Skill;

public sealed class SelectFieldsFromSkillSpecification :
    GenericSpecification<SkillEntity>
{
    public SelectFieldsFromSkillSpecification Ver1()
    {
        SelectExpression = skill => new()
        {
            Id = skill.Id,
            Name = skill.Name
        };

        return this;
    }

    public SelectFieldsFromSkillSpecification Ver2()
    {
        SelectExpression = skill => new()
        {
            Id = skill.Id,
            Name = skill.Name,
            DeletedAt = skill.DeletedAt,
            DeletedBy = skill.DeletedBy
        };

        return this;
    }

    public SelectFieldsFromSkillSpecification Ver3()
    {
        SelectExpression = skill => new()
        {
            Id = skill.Id
        };

        return this;
    }
}

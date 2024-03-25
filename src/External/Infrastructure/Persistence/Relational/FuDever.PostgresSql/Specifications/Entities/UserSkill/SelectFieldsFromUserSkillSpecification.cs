using FuDever.Domain.EntityBuilders.Skill;
using FuDever.Domain.EntityBuilders.UserSkill;
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
        UserSkillForDatabaseRetrievingBuilder userSkillBuilder = new();
        SkillForDatabaseRetrievingBuilder skillBuilder = new();

        SelectExpression = userSkill => userSkillBuilder
            .WithSkillId(userSkill.SkillId)
            .WithSkill(skillBuilder
                .WithName(userSkill.Skill.Name)
                .Complete())
            .Complete();

        return this;
    }

    public ISelectFieldsFromUserSkillSpecification Ver2()
    {
        UserSkillForDatabaseRetrievingBuilder builder = new();

        SelectExpression = userSkill => builder
            .WithUserId(userSkill.UserId)
            .Complete();

        return this;
    }
}

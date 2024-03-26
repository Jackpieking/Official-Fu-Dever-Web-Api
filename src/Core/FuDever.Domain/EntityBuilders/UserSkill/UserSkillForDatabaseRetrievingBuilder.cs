using FuDever.Domain.EntityBuilders.UserSkill.Others;
using System;

namespace FuDever.Domain.EntityBuilders.UserSkill;

/// <summary>
///     User skill for database retrieving builder.
/// </summary>
public sealed class UserSkillForDatabaseRetrievingBuilder :
    Entities.UserSkill,
    IBaseUserSkillBuilder,
    IUserSkillBuilder<UserSkillForDatabaseRetrievingBuilder>,
    IUserSkillNavigationPropertyBuilder<UserSkillForDatabaseRetrievingBuilder>
{
    public void Clear()
    {
        SkillId = Guid.Empty;
        UserId = Guid.Empty;
        Skill = default;
    }

    public Entities.UserSkill Complete()
    {
        return new()
        {
            SkillId = SkillId,
            UserId = UserId,
            Skill = Skill
        };
    }

    public UserSkillForDatabaseRetrievingBuilder WithSkill(Entities.Skill skill)
    {
        Skill = skill;

        return this;
    }

    public UserSkillForDatabaseRetrievingBuilder WithSkillId(Guid skillId)
    {
        SkillId = skillId;

        return this;
    }

    public UserSkillForDatabaseRetrievingBuilder WithUserId(Guid userId)
    {
        UserId = userId;

        return this;
    }
}

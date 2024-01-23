using Domain.Specifications.Base;
using Domain.Specifications.Entities.Skill;
using Persistence.Commons;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.Skill;

/// <summary>
///     Represent implementation of skill temporarily
///     removed specification.
/// </summary>
internal sealed class SkillTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Skill>,
    ISkillTemporarilyRemovedSpecification
{
    internal SkillTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = skill =>
            skill.RemovedBy != Guid.Empty &&
            skill.RemovedAt != minDateTimeInDatabase;
    }
}

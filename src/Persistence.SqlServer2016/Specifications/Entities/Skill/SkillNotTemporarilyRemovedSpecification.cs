using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.Skill;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.Skill;

/// <summary>
///     Represent implementation of skill not temporarily
///     removed specification.
/// </summary>
internal sealed class SkillNotTemporarilyRemovedSpecification :
    BaseSpecification<Domain.Entities.Skill>,
    ISkillNotTemporarilyRemovedSpecification
{
    internal SkillNotTemporarilyRemovedSpecification()
    {
        var minDateTimeInDatabase = CustomConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = skill =>
            skill.RemovedBy == Guid.Empty &&
            skill.RemovedAt == minDateTimeInDatabase;
    }
}

using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Skill;
using FuDever.SqlServer.Commons;

namespace FuDever.SqlServer.Specifications.Entities.Skill;

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
        var minDateTimeInDatabase = CommonConstant.DbDefaultValue.MIN_DATE_TIME;

        WhereExpression = skill =>
            skill.RemovedBy == Application.Commons.CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID &&
            skill.RemovedAt == minDateTimeInDatabase;
    }
}

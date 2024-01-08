using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Skill;

public sealed class NoTrackingOnSkillSpecification :
    GenericSpecification<SkillEntity>
{
    public NoTrackingOnSkillSpecification()
    {
        IsAsNoTracking = true;
    }
}

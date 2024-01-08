using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserSkill;

public sealed class SelectFieldsFromUserSkillSpecification : GenericSpecification<UserSkillEntity>
{
    public SelectFieldsFromUserSkillSpecification Ver1()
    {
        SelectExpression = userSkill => new()
        {
            SkillId = userSkill.SkillId,
            SkillEntity = new()
            {
                Name = userSkill.SkillEntity.Name
            }
        };

        return this;
    }

    public SelectFieldsFromUserSkillSpecification Ver2()
    {
        SelectExpression = userSkill => new()
        {
            UserId = userSkill.UserId
        };

        return this;
    }
}

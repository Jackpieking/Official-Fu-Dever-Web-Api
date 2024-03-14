using FuDever.Domain.Specifications.Base;

namespace FuDever.Domain.Specifications.Entities.UserSkill;

/// <summary>
///     Represent select fields from the "UserSkills" table specification.
/// </summary>
public interface ISelectFieldsFromUserSkillSpecification : IBaseSpecification<Domain.Entities.UserSkill>
{
    ISelectFieldsFromUserSkillSpecification Ver1();

    ISelectFieldsFromUserSkillSpecification Ver2();
}

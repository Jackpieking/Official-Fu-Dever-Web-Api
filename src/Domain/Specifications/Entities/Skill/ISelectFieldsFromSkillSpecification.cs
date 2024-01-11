using Domain.Specifications.Base;

namespace Domain.Specifications.Entities.Skill;

/// <summary>
///     Represent select fields from the "Skills" table specification.
/// </summary>
public interface ISelectFieldsFromSkillSpecification : IBaseSpecification<Domain.Entities.Skill>
{
    ISelectFieldsFromSkillSpecification Ver1();

    ISelectFieldsFromSkillSpecification Ver2();

    ISelectFieldsFromSkillSpecification Ver3();
}

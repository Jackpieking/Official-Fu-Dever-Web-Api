using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Skill.Manager.Contratcs;

public interface ISkillSpecificationManager
{
    IsSkillNotSoftRemovedSpecification IsSkillNotSoftRemovedSpecification { get; }

    IsSkillSoftRemovedSpecification IsSkillSoftRemovedSpecification { get; }

    NoTrackingOnSkillSpecification NoTrackingOnSkillSpecification { get; }

    SelectFieldsFromSkillSpecification SelectFieldsFromSkillSpecification { get; }

    SkillByIdSpecification SkillByIdSpecification(Guid skillId);

    SkillByNameSpecification SkillByNameSpecification(
        string skillName,
        bool isCaseSensitive = false);
}

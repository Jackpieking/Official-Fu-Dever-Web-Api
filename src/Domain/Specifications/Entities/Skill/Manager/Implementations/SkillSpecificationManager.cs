using FuDeverWebApi.DataAccess.Specifications.Entites.Skill.Manager.Contratcs;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Skill.Implementations;

public sealed class SkillSpecificationManager :
    ISkillSpecificationManager
{
    // Backing fields
    private IsSkillNotSoftRemovedSpecification _isSkillNotSoftRemovedSpecification;
    private IsSkillSoftRemovedSpecification _isSkillSoftRemovedSpecification;
    private NoTrackingOnSkillSpecification _noTrackingOnSkillSpecification;
    private SelectFieldsFromSkillSpecification _selectFieldsFromSkillSpecification;

    public IsSkillNotSoftRemovedSpecification IsSkillNotSoftRemovedSpecification
    {
        get
        {
            _isSkillNotSoftRemovedSpecification ??= new();

            return _isSkillNotSoftRemovedSpecification;
        }
    }

    public IsSkillSoftRemovedSpecification IsSkillSoftRemovedSpecification
    {
        get
        {
            _isSkillSoftRemovedSpecification ??= new();

            return _isSkillSoftRemovedSpecification;
        }
    }

    public NoTrackingOnSkillSpecification NoTrackingOnSkillSpecification
    {
        get
        {
            _noTrackingOnSkillSpecification ??= new();

            return _noTrackingOnSkillSpecification;
        }
    }

    public SelectFieldsFromSkillSpecification SelectFieldsFromSkillSpecification
    {
        get
        {
            _selectFieldsFromSkillSpecification ??= new();

            return _selectFieldsFromSkillSpecification;
        }
    }

    public SkillByIdSpecification SkillByIdSpecification(Guid skillId)
    {
        return new(skillId: skillId);
    }

    public SkillByNameSpecification SkillByNameSpecification(
        string skillName,
        bool isCaseSensitive = false)
    {
        return new(
            skillName: skillName,
            isCaseSensitive: isCaseSensitive);
    }
}

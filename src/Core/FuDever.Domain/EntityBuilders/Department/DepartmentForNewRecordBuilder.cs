using System;
using FuDever.Domain.EntityBuilders.Department.Others;

namespace FuDever.Domain.EntityBuilders.Department;

/// <summary>
///     Department for new record builder.
/// </summary>
public sealed class DepartmentForNewRecordBuilder :
    Entities.Department,
    IBaseDepartmentBuilder,
    IDepartmentBuilder<DepartmentForNewRecordBuilder>
{
    public Entities.Department Complete()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            RemovedAt = RemovedAt,
            RemovedBy = RemovedBy
        };
    }

    public DepartmentForNewRecordBuilder WithId(Guid departmentId)
    {
        // Validate department Id.
        if (departmentId == Guid.Empty)
        {
            return default;
        }

        Id = departmentId;

        return this;
    }

    public DepartmentForNewRecordBuilder WithName(string departmentName)
    {
        // Validate department name.
        if (string.IsNullOrWhiteSpace(value: departmentName) ||
            departmentName.Length > Entities.Department.Metadata.Name.MaxLength ||
            departmentName.Length < Entities.Department.Metadata.Name.MinLength)
        {
            return default;
        }

        Name = departmentName;

        return this;
    }

    public DepartmentForNewRecordBuilder WithRemovedAt(DateTime departmentRemovedAt)
    {
        // Validate department removed at.
        if (departmentRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        RemovedAt = departmentRemovedAt;

        return this;
    }

    public DepartmentForNewRecordBuilder WithRemovedBy(Guid departmentRemovedBy)
    {
        // Validate department removed by.
        if (departmentRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = departmentRemovedBy;

        return this;
    }
}

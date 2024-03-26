using FuDever.Domain.EntityBuilders.Department.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Department;

/// <summary>
///     Department for database seeding builder.
/// </summary>
public sealed class DepartmentForDatabaseSeedingBuilder :
    Entities.Department,
    IBaseDepartmentBuilder,
    IDepartmentBuilder<DepartmentForDatabaseSeedingBuilder>
{
    public void Clear()
    {
        Id = Guid.Empty;
        Name = default;
        RemovedAt = default;
        RemovedBy = Guid.Empty;
    }

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

    public DepartmentForDatabaseSeedingBuilder WithId(Guid departmentId)
    {
        // Validate department Id.
        if (departmentId == Guid.Empty)
        {
            return default;
        }

        Id = departmentId;

        return this;
    }

    public DepartmentForDatabaseSeedingBuilder WithName(string departmentName)
    {
        Name = departmentName;

        return this;
    }

    public DepartmentForDatabaseSeedingBuilder WithRemovedAt(DateTime departmentRemovedAt)
    {
        // Validate department removed at.
        if (departmentRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        RemovedAt = departmentRemovedAt;

        return this;
    }

    public DepartmentForDatabaseSeedingBuilder WithRemovedBy(Guid departmentRemovedBy)
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

using FuDever.Domain.EntityBuilders.Department.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Department;

/// <summary>
///     Department for database retrieving builder.
/// </summary>
public sealed class DepartmentForDatabaseRetrievingBuilder :
    Entities.Department,
    IBaseDepartmentBuilder,
    IDepartmentBuilder<DepartmentForDatabaseRetrievingBuilder>
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

    public DepartmentForDatabaseRetrievingBuilder WithId(Guid departmentId)
    {
        Id = departmentId;

        return this;
    }

    public DepartmentForDatabaseRetrievingBuilder WithName(string departmentName)
    {
        Name = departmentName;

        return this;
    }

    public DepartmentForDatabaseRetrievingBuilder WithRemovedAt(DateTime departmentRemovedAt)
    {
        RemovedAt = departmentRemovedAt;

        return this;
    }

    public DepartmentForDatabaseRetrievingBuilder WithRemovedBy(Guid departmentRemovedBy)
    {
        RemovedBy = departmentRemovedBy;

        return this;
    }
}

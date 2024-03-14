using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Commons;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.SqlServer.Repositories;

/// <summary>
///     Implementation of department repository.
/// </summary>
internal sealed class DepartmentRepository :
    BaseRepository<Department>,
    IDepartmentRepository
{
    internal DepartmentRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkUpdateByDepartmentIdVer1Async(
        Guid departmentId,
        DateTime departmentRemovedAt,
        Guid departmentRemovedBy,
        CancellationToken cancellationToken)
    {
        if (departmentId == Guid.Empty ||
            departmentRemovedAt < CommonConstant.DbDefaultValue.MIN_DATE_TIME ||
            departmentRemovedAt > DateTime.UtcNow ||
            departmentRemovedBy == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: department => department.Id == departmentId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        department => department.RemovedAt,
                        departmentRemovedAt)
                    .SetProperty(
                        department => department.RemovedBy,
                        departmentRemovedBy),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkUpdateByDepartmentIdVer2Async(
        Guid departmentId,
        string departmentName,
        CancellationToken cancellationToken)
    {
        if (departmentId == Guid.Empty ||
            string.IsNullOrWhiteSpace(value: departmentName) ||
            departmentName.Length > Department.Metadata.Name.MaxLength ||
            departmentName.Length < Department.Metadata.Name.MinLength)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: department => department.Id == departmentId)
            .ExecuteUpdateAsync(
                setPropertyCalls: setter => setter
                    .SetProperty(
                        department => department.Name,
                        departmentName),
                cancellationToken: cancellationToken);
    }

    public Task<int> BulkRemoveByDepartmentIdAsync(
        Guid departmentId,
        CancellationToken cancellationToken)
    {
        if (departmentId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: department => department.Id == departmentId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}
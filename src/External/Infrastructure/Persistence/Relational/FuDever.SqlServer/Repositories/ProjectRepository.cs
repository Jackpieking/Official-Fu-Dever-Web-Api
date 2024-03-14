using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.SqlServer.Repositories;

/// <summary>
///     Implementation of project repository.
/// </summary>
internal sealed class ProjectRepository :
    BaseRepository<Project>,
    IProjectRepository
{
    internal ProjectRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkRemoveByAuthorIdAsync(
        Guid projectAuthorId,
        CancellationToken cancellationToken)
    {
        if (projectAuthorId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: project => project.AuthorId == projectAuthorId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}

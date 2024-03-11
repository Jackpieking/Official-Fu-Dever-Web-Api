using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.RelationalDatabase.SqlServer.Data;
using Persistence.RelationalDatabase.SqlServer.Repositories.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.RelationalDatabase.SqlServer.Repositories;

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

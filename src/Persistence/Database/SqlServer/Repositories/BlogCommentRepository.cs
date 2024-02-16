using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Database.SqlServer.Data;
using Persistence.Database.SqlServer.Repositories.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Database.SqlServer.Repositories;

/// <summary>
///     Implementation of blog comment repository.
/// </summary>
internal sealed class BlogCommentRepository :
    BaseRepository<BlogComment>,
    IBlogCommentRepository
{
    internal BlogCommentRepository(FuDeverContext context) : base(context: context)
    {
    }

    public Task<int> BulkRemoveByAuthorIdAsync(
        Guid authorId,
        CancellationToken cancellationToken)
    {
        if (authorId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: blogComment => blogComment.AuthorId == authorId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}
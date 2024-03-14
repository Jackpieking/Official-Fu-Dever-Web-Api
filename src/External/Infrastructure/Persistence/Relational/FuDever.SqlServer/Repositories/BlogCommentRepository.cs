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
        Guid blogCommentAuthorId,
        CancellationToken cancellationToken)
    {
        if (blogCommentAuthorId == Guid.Empty)
        {
            return Task.FromResult<int>(result: default);
        }

        return _dbSet
            .Where(predicate: blogComment => blogComment.AuthorId == blogCommentAuthorId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

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
        return _dbSet
            .Where(predicate: blogComment => blogComment.AuthorId == authorId)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}
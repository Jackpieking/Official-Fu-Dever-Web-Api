using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

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
}

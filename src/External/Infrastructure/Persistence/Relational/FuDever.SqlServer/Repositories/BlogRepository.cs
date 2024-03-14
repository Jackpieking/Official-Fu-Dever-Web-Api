using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

/// <summary>
///     Implementation of blog repository.
/// </summary>
internal sealed class BlogRepository :
    BaseRepository<Blog>,
    IBlogRepository
{
    internal BlogRepository(FuDeverContext context) : base(context: context)
    {
    }
}

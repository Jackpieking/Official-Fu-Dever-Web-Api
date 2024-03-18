using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

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

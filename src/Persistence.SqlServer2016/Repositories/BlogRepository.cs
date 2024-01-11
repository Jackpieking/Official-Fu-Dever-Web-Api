using Domain.Data;
using Domain.Entities;
using Domain.Repositories;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

/// <summary>
///     Implementation of blog repository.
/// </summary>
internal sealed class BlogRepository :
    BaseRepository<Blog>,
    IBlogRepository
{
    internal BlogRepository(IFuDeverContext context) : base(context: context)
    {
    }
}

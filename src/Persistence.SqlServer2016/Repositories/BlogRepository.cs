using Domain.Data;
using Domain.Entities;
using Domain.Repositories;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

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

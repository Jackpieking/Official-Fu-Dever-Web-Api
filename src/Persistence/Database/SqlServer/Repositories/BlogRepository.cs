using Domain.Entities;
using Domain.Repositories;
using Persistence.Database.SqlServer.Data;
using Persistence.Database.SqlServer.Repositories.Base;

namespace Persistence.Database.SqlServer.Repositories;

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

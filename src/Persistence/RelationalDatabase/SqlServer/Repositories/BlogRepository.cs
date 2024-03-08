using Domain.Entities;
using Domain.Repositories;
using Persistence.RelationalDatabase.SqlServer.Data;
using Persistence.RelationalDatabase.SqlServer.Repositories.Base;

namespace Persistence.RelationalDatabase.SqlServer.Repositories;

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

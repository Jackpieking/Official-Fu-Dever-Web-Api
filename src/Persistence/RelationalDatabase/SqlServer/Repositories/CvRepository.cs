using Domain.Entities;
using Domain.Repositories;
using Persistence.RelationalDatabase.SqlServer.Data;
using Persistence.RelationalDatabase.SqlServer.Repositories.Base;

namespace Persistence.RelationalDatabase.SqlServer.Repositories;

/// <summary>
///     Implementation of cv repository.
/// </summary>
internal sealed class CvRepository :
    BaseRepository<Cv>,
    ICvRepository
{
    internal CvRepository(FuDeverContext context) : base(context: context)
    {
    }
}

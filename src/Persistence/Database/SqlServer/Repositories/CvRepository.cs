using Domain.Entities;
using Domain.Repositories;
using Persistence.Database.SqlServer.Data;
using Persistence.Database.SqlServer.Repositories.Base;

namespace Persistence.Database.SqlServer.Repositories;

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

using Domain.Entities;
using Domain.Repositories;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

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

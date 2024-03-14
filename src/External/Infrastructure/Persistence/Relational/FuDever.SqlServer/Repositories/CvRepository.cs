using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

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

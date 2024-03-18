using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

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

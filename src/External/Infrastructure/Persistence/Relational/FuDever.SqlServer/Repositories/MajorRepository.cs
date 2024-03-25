using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

/// <summary>
///     Implementation of major repository.
/// </summary>
internal sealed class MajorRepository :
    BaseRepository<Major>,
    IMajorRepository
{
    internal MajorRepository(FuDeverContext context) : base(context: context)
    {
    }
}
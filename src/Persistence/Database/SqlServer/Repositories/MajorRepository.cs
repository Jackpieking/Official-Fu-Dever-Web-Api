using Domain.Entities;
using Domain.Repositories;
using Persistence.Database.SqlServer.Data;
using Persistence.Database.SqlServer.Repositories.Base;

namespace Persistence.Database.SqlServer.Repositories;

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
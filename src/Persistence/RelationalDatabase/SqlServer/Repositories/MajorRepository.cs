using Domain.Entities;
using Domain.Repositories;
using Persistence.RelationalDatabase.SqlServer.Data;
using Persistence.RelationalDatabase.SqlServer.Repositories.Base;

namespace Persistence.RelationalDatabase.SqlServer.Repositories;

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
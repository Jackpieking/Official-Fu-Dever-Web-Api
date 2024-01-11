using Domain.Data;
using Domain.Entities;
using Domain.Repositories;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

/// <summary>
///     Implementation of major repository.
/// </summary>
internal sealed class MajorRepository :
    BaseRepository<Major>,
    IMajorRepository
{
    internal MajorRepository(IFuDeverContext context) : base(context: context)
    {
    }
}
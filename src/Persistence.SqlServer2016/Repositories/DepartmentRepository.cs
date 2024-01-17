using Domain.Entities;
using Domain.Repositories;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories.Base;

namespace Persistence.SqlServer2016.Repositories;

/// <summary>
///     Implementation of department repository.
/// </summary>
internal sealed class DepartmentRepository :
    BaseRepository<Department>,
    IDepartmentRepository
{
    internal DepartmentRepository(FuDeverContext context) : base(context: context)
    {
    }
}

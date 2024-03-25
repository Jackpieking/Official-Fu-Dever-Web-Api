using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

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

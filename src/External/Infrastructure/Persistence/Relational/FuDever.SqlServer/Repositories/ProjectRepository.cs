using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

/// <summary>
///     Implementation of project repository.
/// </summary>
internal sealed class ProjectRepository :
    BaseRepository<Project>,
    IProjectRepository
{
    internal ProjectRepository(FuDeverContext context) : base(context: context)
    {
    }
}

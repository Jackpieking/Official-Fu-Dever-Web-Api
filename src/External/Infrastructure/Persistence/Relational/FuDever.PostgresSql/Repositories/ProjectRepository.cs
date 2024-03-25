using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

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

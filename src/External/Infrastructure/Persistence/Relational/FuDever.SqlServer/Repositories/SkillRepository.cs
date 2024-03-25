using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

/// <summary>
///     Implementation of skill repository.
/// </summary>
internal sealed class SkillRepository :
    BaseRepository<Skill>,
    ISkillRepository
{
    internal SkillRepository(FuDeverContext context) : base(context: context)
    {
    }
}

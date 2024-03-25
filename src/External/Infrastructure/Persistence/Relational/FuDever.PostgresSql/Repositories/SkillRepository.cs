using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

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

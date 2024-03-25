using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.PostgresSql.Data;
using FuDever.PostgresSql.Repositories.Base;

namespace FuDever.PostgresSql.Repositories;

/// <summary>
///     Implementation of user skill repository.
/// </summary>
internal sealed class UserSkillRepository :
    BaseRepository<UserSkill>,
    IUserSkillRepository
{
    internal UserSkillRepository(FuDeverContext context) : base(context: context)
    {
    }
}

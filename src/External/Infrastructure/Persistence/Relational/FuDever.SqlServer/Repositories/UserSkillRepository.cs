using FuDever.Domain.Entities;
using FuDever.Domain.Repositories;
using FuDever.SqlServer.Data;
using FuDever.SqlServer.Repositories.Base;

namespace FuDever.SqlServer.Repositories;

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

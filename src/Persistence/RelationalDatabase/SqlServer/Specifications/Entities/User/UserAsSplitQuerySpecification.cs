using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user as split query specification.
/// </summary>
internal sealed class UserAsSplitQuerySpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserAsSplitQuerySpecification
{
    internal UserAsSplitQuerySpecification()
    {
        IsAsSplitQuery = true;
    }
}

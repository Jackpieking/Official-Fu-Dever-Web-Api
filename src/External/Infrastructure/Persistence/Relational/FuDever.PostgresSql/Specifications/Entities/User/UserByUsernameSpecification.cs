using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.User;

namespace FuDever.PostgresSql.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user by username specification.
/// </summary>
internal sealed class UserByUsernameSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserByUsernameSpecification
{
    internal UserByUsernameSpecification(string username)
    {
        WhereExpression = user => user.UserName.Equals(username);
    }
}
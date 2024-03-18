using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.User;

namespace FuDever.PostgresSql.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user by user email
///     specification.
/// </summary>
internal sealed class UserByEmailSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserByEmailSpecification
{
    internal UserByEmailSpecification(string email)
    {
        WhereExpression = user => user.Email.Equals(email);
    }
}

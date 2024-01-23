using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;

namespace Persistence.Database.SqlServer.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user by phone number
///     specification.
/// </summary>
internal sealed class UserByPhoneNumberSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserByPhoneNumberSpecification
{
    internal UserByPhoneNumberSpecification(string phoneNumber)
    {
        WhereExpression = user => user.PhoneNumber.Equals(phoneNumber);
    }
}

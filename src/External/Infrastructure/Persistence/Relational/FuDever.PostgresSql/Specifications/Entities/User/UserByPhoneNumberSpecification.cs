using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.User;

namespace FuDever.PostgresSql.Specifications.Entities.User;

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

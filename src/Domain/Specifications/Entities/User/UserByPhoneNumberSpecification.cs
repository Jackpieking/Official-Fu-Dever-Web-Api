using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class UserByPhoneNumberSpecification :
    GenericSpecification<AppUserEntity>
{
    public UserByPhoneNumberSpecification(string phoneNumber)
    {
        Criteria = user => user.PhoneNumber.Equals(phoneNumber);
    }
}

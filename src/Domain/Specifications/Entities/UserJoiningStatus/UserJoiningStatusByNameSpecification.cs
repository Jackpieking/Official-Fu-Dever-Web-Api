using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserJoiningStatus;

public sealed class UserJoiningStatusByNameSpecification :
    GenericSpecification<UserJoiningStatusEntity>
{
    public UserJoiningStatusByNameSpecification(string userJoiningStatusName)
    {
        Criteria = userJoiningStatus => userJoiningStatus.Name.Equals(userJoiningStatusName);
    }
}

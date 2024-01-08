using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserJoiningStatus;

public sealed class SelectFieldsFromUserJoiningStatusSpecification :
    GenericSpecification<UserJoiningStatusEntity>
{
    public SelectFieldsFromUserJoiningStatusSpecification Ver1()
    {
        SelectExpression = userJoiningStatus => new()
        {
            Id = userJoiningStatus.Id
        };

        return this;
    }
}

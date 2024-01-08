using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserPlatform;

public sealed class SelectFieldsFromUserPlatformSpecification : GenericSpecification<UserPlatformEntity>
{
    public SelectFieldsFromUserPlatformSpecification Ver1()
    {
        SelectExpression = userPlatform => new()
        {
            UserId = userPlatform.UserId
        };

        return this;
    }
}

using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserHobby;

public sealed class SelectFieldsFromUserHobbySpecification :
    GenericSpecification<UserHobbyEntity>
{
    public SelectFieldsFromUserHobbySpecification Ver1()
    {
        SelectExpression = userHobby => new()
        {
            HobbyId = userHobby.HobbyId,
            HobbyEntity = new()
            {
                Name = userHobby.HobbyEntity.Name
            }
        };

        return this;
    }
}

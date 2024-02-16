using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserHobby;

namespace Persistence.Database.SqlServer.Specifications.Entities.UserHobby;

/// <summary>
///     Represent implementation of select fields from the "UserHobbies"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromUserHobbySpecification :
    BaseSpecification<Domain.Entities.UserHobby>,
    ISelectFieldsFromUserHobbySpecification
{
    public ISelectFieldsFromUserHobbySpecification Ver1()
    {
        SelectExpression = userHobby => Domain.Entities.UserHobby.InitVer2(
            userHobby.HobbyId,
            Domain.Entities.Hobby.InitVer4(userHobby.Hobby.Name));

        return this;
    }
}

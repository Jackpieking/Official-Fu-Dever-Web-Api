using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserHobby;

namespace Persistence.SqlServer2016.Specifications.Entities.UserHobby;

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
        SelectExpression = userHobby => new()
        {
            HobbyId = userHobby.HobbyId,
            Hobby = new()
            {
                Name = userHobby.Hobby.Name
            }
        };

        return this;
    }
}

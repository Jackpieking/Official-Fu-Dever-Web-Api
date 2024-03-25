using FuDever.Domain.EntityBuilders.Hobby;
using FuDever.Domain.EntityBuilders.UserHobby;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserHobby;

namespace FuDever.SqlServer.Specifications.Entities.UserHobby;

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
        UserHobbyForDatabaseRetrievingBuilder userHobbyBuilder = new();
        HobbyForDatabaseRetrievingBuilder hobbyBuilder = new();

        SelectExpression = userHobby => userHobbyBuilder
            .WithHobbyId(userHobby.HobbyId)
            .WithHobby(hobbyBuilder
                .WithName(userHobby.Hobby.Name)
                .Complete())
            .Complete();

        return this;
    }

    public ISelectFieldsFromUserHobbySpecification Ver2()
    {
        UserHobbyForDatabaseRetrievingBuilder builder = new();

        SelectExpression = userHobby => builder
            .WithUserId(userHobby.UserId)
            .Complete();

        return this;
    }
}

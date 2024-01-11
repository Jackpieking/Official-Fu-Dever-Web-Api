using Domain.Specifications.Base;

namespace Domain.Specifications.Entities.UserHobby;

/// <summary>
///     Represent select fields from the "UserHobbies" table specification.
/// </summary>
public interface ISelectFieldsFromUserHobbySpecification : IBaseSpecification<Domain.Entities.UserHobby>
{
    ISelectFieldsFromUserHobbySpecification Ver1();
}

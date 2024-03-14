using FuDever.Domain.Specifications.Base;

namespace FuDever.Domain.Specifications.Entities.UserHobby;

/// <summary>
///     Represent select fields from the "UserHobbies" table specification.
/// </summary>
public interface ISelectFieldsFromUserHobbySpecification : IBaseSpecification<Domain.Entities.UserHobby>
{
    ISelectFieldsFromUserHobbySpecification Ver1();

    ISelectFieldsFromUserHobbySpecification Ver2();
}

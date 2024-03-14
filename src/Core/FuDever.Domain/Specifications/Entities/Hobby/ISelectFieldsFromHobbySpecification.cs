using FuDever.Domain.Specifications.Base;

namespace FuDever.Domain.Specifications.Entities.Hobby;

/// <summary>
///     Represent select fields from the "Hobbies" table specification.
/// </summary>
public interface ISelectFieldsFromHobbySpecification : IBaseSpecification<Domain.Entities.Hobby>
{
    ISelectFieldsFromHobbySpecification Ver1();

    ISelectFieldsFromHobbySpecification Ver2();

    ISelectFieldsFromHobbySpecification Ver3();

    ISelectFieldsFromHobbySpecification Ver4();
}

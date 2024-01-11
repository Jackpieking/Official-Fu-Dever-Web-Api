using Domain.Specifications.Base;

namespace Domain.Specifications.Entities.Major;

/// <summary>
///     Represent select fields from the "Majors" table specification.
/// </summary>
public interface ISelectFieldsFromMajorSpecification : IBaseSpecification<Domain.Entities.Major>
{
    ISelectFieldsFromMajorSpecification Ver1();

    ISelectFieldsFromMajorSpecification Ver2();
}

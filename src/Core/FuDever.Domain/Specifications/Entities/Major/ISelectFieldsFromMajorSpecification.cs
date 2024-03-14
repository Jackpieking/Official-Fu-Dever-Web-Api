using FuDever.Domain.Specifications.Base;

namespace FuDever.Domain.Specifications.Entities.Major;

/// <summary>
///     Represent select fields from the "Majors" table specification.
/// </summary>
public interface ISelectFieldsFromMajorSpecification : IBaseSpecification<Domain.Entities.Major>
{
    ISelectFieldsFromMajorSpecification Ver1();

    ISelectFieldsFromMajorSpecification Ver2();

    ISelectFieldsFromMajorSpecification Ver3();
}

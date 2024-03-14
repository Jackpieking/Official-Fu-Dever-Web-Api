using FuDever.Domain.Specifications.Base;

namespace FuDever.Domain.Specifications.Entities.Cv;

/// <summary>
///     Represent select fields from "Cvs" table specification.
/// </summary>
public interface ISelectFieldsFromCvSpecification : IBaseSpecification<Domain.Entities.Cv>
{
    ISelectFieldsFromCvSpecification Ver1();
}

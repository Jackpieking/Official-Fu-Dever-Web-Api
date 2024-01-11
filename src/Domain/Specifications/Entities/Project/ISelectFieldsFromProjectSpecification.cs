using Domain.Specifications.Base;

namespace Domain.Specifications.Entities.Project;

/// <summary>
///     Represent select fields from the "Projects" table specification.
/// </summary>
public interface ISelectFieldsFromProjectSpecification : IBaseSpecification<Domain.Entities.Project>
{
    ISelectFieldsFromProjectSpecification Ver1();
}

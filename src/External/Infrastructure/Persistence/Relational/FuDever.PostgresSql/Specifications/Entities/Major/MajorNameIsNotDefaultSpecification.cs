using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Major;

namespace FuDever.PostgresSql.Specifications.Entities.Major;

/// <summary>
///     Represent implementation of major name is
///     not default specification.
/// </summary>
internal sealed class MajorNameIsNotDefaultSpecification :
    BaseSpecification<Domain.Entities.Major>,
    IMajorNameIsNotDefaultSpecification
{
    public MajorNameIsNotDefaultSpecification()
    {
        WhereExpression = major => !major.Name.Equals(string.Empty);
    }
}

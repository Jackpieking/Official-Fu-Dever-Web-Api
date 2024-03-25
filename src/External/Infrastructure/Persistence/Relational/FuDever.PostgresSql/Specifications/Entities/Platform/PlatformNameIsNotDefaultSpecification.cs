using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Platform;

namespace FuDever.PostgresSql.Specifications.Entities.Platform;

/// <summary>
///     Represent implementation of platform name
///     is not default specification.
/// </summary>
internal sealed class PlatformNameIsNotDefaultSpecification :
    BaseSpecification<Domain.Entities.Platform>,
    IPlatformNameIsNotDefaultSpecification
{
    public PlatformNameIsNotDefaultSpecification()
    {
        WhereExpression = platform => !platform.Name.Equals(string.Empty);
    }
}

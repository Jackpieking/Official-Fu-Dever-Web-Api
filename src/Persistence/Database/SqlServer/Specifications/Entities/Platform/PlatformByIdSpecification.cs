using Domain.Specifications.Base;
using Domain.Specifications.Entities.Platform;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.Platform;

/// <summary>
///     Represent implementation of platform by platform id specification.
/// </summary>
internal sealed class PlatformByIdSpecification :
    BaseSpecification<Domain.Entities.Platform>,
    IPlatformByIdSpecification
{
    internal PlatformByIdSpecification(Guid platformId)
    {
        WhereExpression = platform => platform.Id == platformId;
    }
}

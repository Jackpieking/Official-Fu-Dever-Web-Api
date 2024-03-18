using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Platform;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.PostgresSql.Specifications.Entities.Platform;

/// <summary>
///     Represent implementation of platform by platform
///     name specification.
/// </summary>
internal sealed class PlatformByNameSpecification :
    BaseSpecification<Domain.Entities.Platform>,
    IPlatformByNameSpecification
{
    internal PlatformByNameSpecification(
        string platformName,
        bool isCaseSensitive)
    {
        if (isCaseSensitive)
        {
            WhereExpression = platform => platform.Name.Equals(platformName);

            return;
        }

        WhereExpression = platform => EF.Functions
            .Collate(
                platform.Name,
                CommonConstant.DbCollation.CASE_INSENSITIVE)
            .Equals(platformName);
    }
}

using Domain.Specifications.Base;
using Domain.Specifications.Entities.Platform;
using Microsoft.EntityFrameworkCore;
using Persistence.Commons;

namespace Persistence.Database.SqlServer.Specifications.Entities.Platform;

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
        if (!isCaseSensitive)
        {
            WhereExpression = platform => platform.Name.Equals(platformName);

            return;
        }

        WhereExpression = platform => EF.Functions
            .Collate(
                platform.Name,
                CommonConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(platformName);
    }
}

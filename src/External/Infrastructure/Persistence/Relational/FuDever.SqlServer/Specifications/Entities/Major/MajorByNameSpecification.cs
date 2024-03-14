using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Major;
using FuDever.SqlServer.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.SqlServer.Specifications.Entities.Major;

internal sealed class MajorByNameSpecification :
    BaseSpecification<Domain.Entities.Major>,
    IMajorByNameSpecification
{
    internal MajorByNameSpecification(
        string majorName,
        bool isCaseSensitive)
    {
        if (!isCaseSensitive)
        {
            WhereExpression = major => major.Name.Equals(majorName);

            return;
        }

        WhereExpression = major => EF.Functions
            .Collate(
                major.Name,
                CommonConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(majorName);
    }
}

using Domain.Specifications.Base;
using Domain.Specifications.Entities.Major;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.Major;

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
                CustomConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(majorName);
    }
}

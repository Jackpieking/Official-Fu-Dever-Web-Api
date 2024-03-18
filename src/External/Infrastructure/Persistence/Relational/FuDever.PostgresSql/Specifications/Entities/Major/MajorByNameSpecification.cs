using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Major;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.PostgresSql.Specifications.Entities.Major;

internal sealed class MajorByNameSpecification :
    BaseSpecification<Domain.Entities.Major>,
    IMajorByNameSpecification
{
    internal MajorByNameSpecification(
        string majorName,
        bool isCaseSensitive)
    {
        if (isCaseSensitive)
        {
            WhereExpression = major => major.Name.Equals(majorName);

            return;
        }

        WhereExpression = major => EF.Functions
            .Collate(
                major.Name,
                CommonConstant.DbCollation.CASE_INSENSITIVE)
            .Equals(majorName);
    }
}

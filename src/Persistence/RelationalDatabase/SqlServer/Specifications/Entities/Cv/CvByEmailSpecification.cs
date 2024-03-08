using Domain.Specifications.Base;
using Domain.Specifications.Entities.Cv;
using Microsoft.EntityFrameworkCore;
using Persistence.RelationalDatabase.SqlServer.Commons;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Cv;

/// <summary>
///     Represent implementation of cv by student email specification.
/// </summary>
internal sealed class CvByEmailSpecification :
    BaseSpecification<Domain.Entities.Cv>,
    ICvByEmailSpecification
{
    internal CvByEmailSpecification(string email)
    {
        WhereExpression = cv => EF.Functions
            .Collate(
                cv.StudentEmail,
                CommonConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(email);
    }
}

using Domain.Specifications.Base;
using Domain.Specifications.Entities.Cv;
using System;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Cv;

/// <summary>
///     Represent implementation of cv by cv id specification.
/// </summary>
internal sealed class CvByIdSpecification :
    BaseSpecification<Domain.Entities.Cv>,
    ICvByIdSpecification
{
    internal CvByIdSpecification(Guid cvId)
    {
        WhereExpression = cv => cv.Id == cvId;
    }
}

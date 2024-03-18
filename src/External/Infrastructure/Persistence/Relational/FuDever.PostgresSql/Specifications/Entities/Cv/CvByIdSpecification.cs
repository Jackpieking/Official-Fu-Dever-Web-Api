using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Cv;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.Cv;

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

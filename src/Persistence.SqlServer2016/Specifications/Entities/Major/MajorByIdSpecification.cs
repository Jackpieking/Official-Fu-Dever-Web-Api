using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.Major;

namespace Persistence.SqlServer2016.Specifications.Entities.Major;

/// <summary>
///     Represent implementation of major by major id specification.
/// </summary>
internal sealed class MajorByIdSpecification :
    BaseSpecification<Domain.Entities.Major>,
    IMajorByIdSpecification
{
    internal MajorByIdSpecification(Guid majorId)
    {
        WhereExpression = major => major.Id == majorId;
    }
}

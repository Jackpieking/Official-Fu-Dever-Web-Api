using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Major;
using System;

namespace FuDever.SqlServer.Specifications.Entities.Major;

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

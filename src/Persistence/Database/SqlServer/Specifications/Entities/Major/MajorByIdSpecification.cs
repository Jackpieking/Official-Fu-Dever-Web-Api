using Domain.Specifications.Base;
using Domain.Specifications.Entities.Major;
using System;

namespace Persistence.Database.SqlServer.Specifications.Entities.Major;

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


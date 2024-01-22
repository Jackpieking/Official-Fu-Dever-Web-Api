using Domain.Specifications.Base;
using Domain.Specifications.Entities.Hobby;
using System;

namespace Persistence.SqlServer2016.Specifications.Entities.Hobby;

/// <summary>
///     Represent implementation of hobby by hobby id specification.
/// </summary>
internal sealed class HobbyByIdSpecification :
    BaseSpecification<Domain.Entities.Hobby>,
    IHobbyByIdSpecification
{
    internal HobbyByIdSpecification(Guid hobbyId)
    {
        WhereExpression = hobby => hobby.Id == hobbyId;
    }
}

using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Hobby;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.Hobby;

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

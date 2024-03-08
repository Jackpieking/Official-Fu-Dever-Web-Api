using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;
using System;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user by major id
///     specification.
/// </summary>
internal sealed class UserByMajorIdSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserByMajorIdSpecification
{
    internal UserByMajorIdSpecification(Guid majorId)
    {
        WhereExpression = user => user.MajorId == majorId;
    }
}


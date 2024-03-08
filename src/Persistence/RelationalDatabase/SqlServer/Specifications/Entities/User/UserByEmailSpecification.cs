using Domain.Specifications.Base;
using Domain.Specifications.Entities.User;
using Microsoft.EntityFrameworkCore;
using Persistence.RelationalDatabase.SqlServer.Commons;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user by user email
///     specification.
/// </summary>
internal sealed class UserByEmailSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserByEmailSpecification
{
    internal UserByEmailSpecification(string email)
    {
        WhereExpression = user => EF.Functions
            .Collate(
                user.Email,
                CommonConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(email);
    }
}

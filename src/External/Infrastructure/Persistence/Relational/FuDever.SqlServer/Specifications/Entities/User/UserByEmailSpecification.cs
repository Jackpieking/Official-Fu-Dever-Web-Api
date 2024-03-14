using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.User;
using FuDever.SqlServer.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.SqlServer.Specifications.Entities.User;

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

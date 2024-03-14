using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.User;
using FuDever.SqlServer.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.SqlServer.Specifications.Entities.User;

/// <summary>
///     Represent implementation of user by username specification.
/// </summary>
internal sealed class UserByUsernameSpecification :
    BaseSpecification<Domain.Entities.User>,
    IUserByUsernameSpecification
{
    internal UserByUsernameSpecification(string username)
    {
        WhereExpression = user => EF.Functions
            .Collate(
                user.UserName,
                CommonConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(username);
    }
}
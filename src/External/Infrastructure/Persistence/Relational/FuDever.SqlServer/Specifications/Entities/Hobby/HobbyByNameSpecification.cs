using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Hobby;
using FuDever.SqlServer.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.SqlServer.Specifications.Entities.Hobby;

/// <summary>
///     Represent implementation of hobby by hobby name specification.
/// </summary>
internal sealed class HobbyByNameSpecification :
    BaseSpecification<Domain.Entities.Hobby>,
    IHobbyByNameSpecification
{
    internal HobbyByNameSpecification(
        string hobbyName,
        bool isCaseSensitive)
    {
        if (!isCaseSensitive)
        {
            WhereExpression = hobby => hobby.Name.Equals(hobbyName);

            return;
        }

        WhereExpression = hobby => EF.Functions
            .Collate(
                hobby.Name,
                CommonConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(hobbyName);
    }
}

using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Hobby;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;

namespace FuDever.PostgresSql.Specifications.Entities.Hobby;

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
        if (isCaseSensitive)
        {
            WhereExpression = hobby => hobby.Name.Equals(hobbyName);

            return;
        }

        WhereExpression = hobby => EF.Functions
            .Collate(
                hobby.Name,
                CommonConstant.DbCollation.CASE_INSENSITIVE)
            .Equals(hobbyName);
    }
}

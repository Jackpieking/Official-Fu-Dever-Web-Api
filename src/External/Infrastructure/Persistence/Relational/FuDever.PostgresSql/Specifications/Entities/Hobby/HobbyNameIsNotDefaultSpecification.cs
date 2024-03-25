using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Hobby;

namespace FuDever.PostgresSql.Specifications.Entities.Hobby;

/// <summary>
///     Represent implementation of hobby name is not
///     default specification.
/// </summary>
internal sealed class HobbyNameIsNotDefaultSpecification :
    BaseSpecification<Domain.Entities.Hobby>,
    IHobbyNameIsNotDefaultSpecification
{
    public HobbyNameIsNotDefaultSpecification()
    {
        WhereExpression = hobby => !hobby.Name.Equals(string.Empty);
    }
}

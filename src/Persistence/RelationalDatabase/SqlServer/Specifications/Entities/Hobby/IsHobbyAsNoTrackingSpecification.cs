using Domain.Specifications.Base;
using Domain.Specifications.Entities.Hobby;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Hobby;

/// <summary>
///     Represent implementation of hobby as no tracking specification.
/// </summary>
internal sealed class HobbyAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.Hobby>,
    IHobbyAsNoTrackingSpecification
{
    internal HobbyAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}

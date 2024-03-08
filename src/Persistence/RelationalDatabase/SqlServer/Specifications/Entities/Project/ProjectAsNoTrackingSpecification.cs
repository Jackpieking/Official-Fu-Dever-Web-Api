using Domain.Specifications.Base;
using Domain.Specifications.Entities.Project;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Project;

/// <summary>
///     Represent implementation of project as no tracking specification.
/// </summary>
internal sealed class ProjectAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.Project>,
    IProjectAsNoTrackingSpecification
{
    internal ProjectAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}

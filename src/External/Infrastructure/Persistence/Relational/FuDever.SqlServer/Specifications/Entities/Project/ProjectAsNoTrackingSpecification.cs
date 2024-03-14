using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Project;

namespace FuDever.SqlServer.Specifications.Entities.Project;

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

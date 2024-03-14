using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Cv;

namespace FuDever.SqlServer.Specifications.Entities.Cv;

/// <summary>
///     Represent implementation of cv as no tracking specification.
/// </summary>
internal sealed class CvAsNoTrackingSpecification :
    BaseSpecification<Domain.Entities.Cv>,
    ICvAsNoTrackingSpecification
{
    internal CvAsNoTrackingSpecification()
    {
        IsAsNoTracking = true;
    }
}

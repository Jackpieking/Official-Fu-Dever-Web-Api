using Domain.Specifications.Base;
using Domain.Specifications.Entities.Cv;

namespace Persistence.SqlServer2016.Specifications.Entities.Cv;

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

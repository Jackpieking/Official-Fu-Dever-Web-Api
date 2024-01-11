using Domain.Specifications.Base;
using Domain.Specifications.Entities.Platform;

namespace Persistence.SqlServer2016.Specifications.Entities.Platform;

/// <summary>
///     Represent implementation of select fields from the "Platforms"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromPlatformSpecification :
    BaseSpecification<Domain.Entities.Platform>,
    ISelectFieldsFromPlatformSpecification
{
    public ISelectFieldsFromPlatformSpecification Ver1()
    {
        SelectExpression = platform => new()
        {
            Id = platform.Id,
            Name = platform.Name
        };

        return this;
    }

    public ISelectFieldsFromPlatformSpecification Ver2()
    {
        SelectExpression = platform => new()
        {
            Id = platform.Id,
            Name = platform.Name,
            RemovedAt = platform.RemovedAt,
            RemovedBy = platform.RemovedBy
        };

        return this;
    }
}

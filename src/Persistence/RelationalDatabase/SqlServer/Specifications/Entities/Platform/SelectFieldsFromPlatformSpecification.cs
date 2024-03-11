using Domain.Specifications.Base;
using Domain.Specifications.Entities.Platform;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Platform;

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
        SelectExpression = platform => Domain.Entities.Platform.InitVer2(
            platform.Id,
            platform.Name);

        return this;
    }

    public ISelectFieldsFromPlatformSpecification Ver2()
    {
        SelectExpression = platform => Domain.Entities.Platform.InitVer1(
            platform.Id,
            platform.Name,
            platform.RemovedAt,
            platform.RemovedBy);

        return this;
    }

    public ISelectFieldsFromPlatformSpecification Ver3()
    {
        SelectExpression = platfrom => Domain.Entities.Platform.InitVer3(platfrom.Name);

        return this;
    }
}

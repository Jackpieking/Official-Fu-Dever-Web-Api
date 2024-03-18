using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Platform;

namespace FuDever.PostgresSql.Specifications.Entities.Platform;

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
        SelectExpression = platform => Domain.Entities.Platform.InitFromDatabaseVer1(
            platform.Id,
            platform.Name);

        return this;
    }

    public ISelectFieldsFromPlatformSpecification Ver2()
    {
        SelectExpression = platform => Domain.Entities.Platform.InitFromDatabaseVer3(
            platform.Id,
            platform.Name,
            platform.RemovedAt,
            platform.RemovedBy);

        return this;
    }

    public ISelectFieldsFromPlatformSpecification Ver3()
    {
        SelectExpression = platfrom => Domain.Entities.Platform.InitFromDatabaseVer2(platfrom.Name);

        return this;
    }
}

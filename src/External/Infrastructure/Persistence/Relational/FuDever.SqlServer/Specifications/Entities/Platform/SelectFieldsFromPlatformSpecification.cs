using FuDever.Domain.EntityBuilders.Platform;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Platform;

namespace FuDever.SqlServer.Specifications.Entities.Platform;

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
        PlatformForDatabaseRetrievingBuilder builder = new();

        SelectExpression = platform => builder
            .WithId(platform.Id)
            .WithName(platform.Name)
            .Complete();

        return this;
    }

    public ISelectFieldsFromPlatformSpecification Ver2()
    {
        PlatformForDatabaseRetrievingBuilder builder = new();

        SelectExpression = platform => builder
            .WithId(platform.Id)
            .WithName(platform.Name)
            .WithRemovedAt(platform.RemovedAt)
            .WithRemovedBy(platform.RemovedBy)
            .Complete();

        return this;
    }

    public ISelectFieldsFromPlatformSpecification Ver3()
    {
        PlatformForDatabaseRetrievingBuilder builder = new();

        SelectExpression = platfrom => builder
            .WithName(platfrom.Name)
            .Complete();

        return this;
    }
}

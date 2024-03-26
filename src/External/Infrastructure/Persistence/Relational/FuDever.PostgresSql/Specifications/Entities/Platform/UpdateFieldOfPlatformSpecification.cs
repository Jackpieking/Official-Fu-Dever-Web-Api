using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Platform;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.Platform;

/// <summary>
///     Represent implementation of update field
///     of platform specification.
/// </summary>
internal sealed class UpdateFieldOfPlatformSpecification :
    BaseSpecification<Domain.Entities.Platform>,
    IUpdateFieldOfPlatformSpecification
{
    public IUpdateFieldOfPlatformSpecification Ver1(
        DateTime platformRemovedAt,
        Guid platformRemovedBy)
    {
        // Validate platform removed time.
        if (platformRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        // Validate platform remover.
        if (platformRemovedBy == Guid.Empty)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall
            .SetProperty(
                platform => platform.RemovedAt,
                platformRemovedAt)
            .SetProperty(
                platform => platform.RemovedBy,
                platformRemovedBy);

        return this;
    }

    public IUpdateFieldOfPlatformSpecification Ver2(string platformName)
    {
        // Validate platform name.
        if (string.IsNullOrWhiteSpace(value: platformName) ||
            platformName.Length > Domain.Entities.Platform.Metadata.Name.MaxLength ||
            platformName.Length < Domain.Entities.Platform.Metadata.Name.MinLength)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall.SetProperty(
            platform => platform.Name,
            platformName);

        return this;
    }
}

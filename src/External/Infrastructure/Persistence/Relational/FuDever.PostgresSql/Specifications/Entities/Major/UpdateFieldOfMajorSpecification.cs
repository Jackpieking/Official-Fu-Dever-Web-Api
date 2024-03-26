using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Major;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.Major;

/// <summary>
///     Represent implementation of update field of
///     major specification.
/// </summary>
internal sealed class UpdateFieldOfMajorSpecification :
    BaseSpecification<Domain.Entities.Major>,
    IUpdateFieldOfMajorSpecification
{
    public IUpdateFieldOfMajorSpecification Ver1(
        DateTime majorRemovedAt,
        Guid majorRemovedBy)
    {
        // Validate department removed time.
        if (majorRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        // Validate department remover.
        if (majorRemovedBy == Guid.Empty)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall
            .SetProperty(
                major => major.RemovedAt,
                majorRemovedAt)
            .SetProperty(
                major => major.RemovedBy,
                majorRemovedBy);

        return this;
    }

    public IUpdateFieldOfMajorSpecification Ver2(string majorName)
    {
        // Validate major name.
        if (string.IsNullOrWhiteSpace(value: majorName) ||
            majorName.Length > Domain.Entities.Major.Metadata.Name.MaxLength ||
            majorName.Length < Domain.Entities.Major.Metadata.Name.MinLength)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall.SetProperty(
            major => major.Name,
            majorName);

        return this;
    }
}

using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Position;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.Position;

/// <summary>
///     Represent implementation of update
///     field of position specification.
/// </summary>
internal sealed class UpdateFieldOfPositionSpecification :
    BaseSpecification<Domain.Entities.Position>,
    IUpdateFieldOfPositionSpecification
{
    public IUpdateFieldOfPositionSpecification Ver1(
        DateTime positionRemovedAt,
        Guid positionRemovedBy)
    {
        // Validate position removed time.
        if (positionRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        // Validate position remover.
        if (positionRemovedBy == Guid.Empty)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall
            .SetProperty(
                position => position.RemovedAt,
                positionRemovedAt)
            .SetProperty(
                position => position.RemovedBy,
                positionRemovedBy);

        return this;
    }

    public IUpdateFieldOfPositionSpecification Ver2(string positionName)
    {
        // Validate position name.
        if (string.IsNullOrWhiteSpace(value: positionName) ||
            positionName.Length > Domain.Entities.Position.Metadata.Name.MaxLength ||
            positionName.Length < Domain.Entities.Position.Metadata.Name.MinLength)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall.SetProperty(
            position => position.Name,
            positionName);

        return this;
    }
}

using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Position.Manager.Contracts;

public interface IPositionSpecificationManager
{
    IsPositionNotSoftRemovedSpecification IsPositionNotSoftRemovedSpecification { get; }

    IsPositionSoftRemovedSpecification IsPositionSoftRemovedSpecification { get; }

    NoTrackingOnPositionSpecification NoTrackingOnPositionSpecification { get; }

    SelectFieldsFromPositionSpecification SelectFieldsFromPositionSpecification { get; }

    PositionByIdSpecification PositionByIdSpecification(Guid positionId);

    PositionByNameSpecification PositionByNameSpecification(
        string positionName,
        bool isCaseSensitive = false);
}

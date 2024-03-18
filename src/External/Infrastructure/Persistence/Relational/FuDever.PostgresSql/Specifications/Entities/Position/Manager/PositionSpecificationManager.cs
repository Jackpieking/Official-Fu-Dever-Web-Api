using FuDever.Domain.Specifications.Entities.Position;
using FuDever.Domain.Specifications.Entities.Position.Manager;
using System;

namespace FuDever.PostgresSql.Specifications.Entities.Position.Manager;

/// <summary>
///     Represent implementation of position specification manager.
/// </summary>
internal sealed class PositionSpecificationManager : IPositionSpecificationManager
{
    // Backing fields
    private IPositionNotTemporarilyRemovedSpecification _positionNotTemporarilyRemovedSpecification;
    private IPositionTemporarilyRemovedSpecification _positionTemporarilyRemovedSpecification;
    private IPositionAsNoTrackingSpecification _positionAsNoTrackingSpecification;
    private ISelectFieldsFromPositionSpecification _selectFieldsFromPositionSpecification;

    public IPositionNotTemporarilyRemovedSpecification PositionNotTemporarilyRemovedSpecification
    {
        get
        {
            _positionNotTemporarilyRemovedSpecification ??= new PositionNotTemporarilyRemovedSpecification();

            return _positionNotTemporarilyRemovedSpecification;
        }
    }

    public IPositionTemporarilyRemovedSpecification PositionTemporarilyRemovedSpecification
    {
        get
        {
            _positionTemporarilyRemovedSpecification ??= new PositionTemporarilyRemovedSpecification();

            return _positionTemporarilyRemovedSpecification;
        }
    }

    public IPositionAsNoTrackingSpecification PositionAsNoTrackingSpecification
    {
        get
        {
            _positionAsNoTrackingSpecification ??= new PositionAsNoTrackingSpecification();

            return _positionAsNoTrackingSpecification;
        }
    }

    public ISelectFieldsFromPositionSpecification SelectFieldsFromPositionSpecification
    {
        get
        {
            _selectFieldsFromPositionSpecification ??= new SelectFieldsFromPositionSpecification();

            return _selectFieldsFromPositionSpecification;
        }
    }

    public IPositionByIdSpecification PositionByIdSpecification(Guid positionId)
    {
        return new PositionByIdSpecification(positionId: positionId);
    }

    public IPositionByNameSpecification PositionByNameSpecification(
        string positionName,
        bool isCaseSensitive)
    {
        return new PositionByNameSpecification(
            positionName: positionName,
            isCaseSensitive: isCaseSensitive);
    }
}

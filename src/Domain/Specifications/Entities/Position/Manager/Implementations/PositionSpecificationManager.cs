using FuDeverWebApi.DataAccess.Specifications.Entites.Position.Manager.Contracts;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Position.Manager.Implementations;

public sealed class PositionSpecificationManager :
    IPositionSpecificationManager
{
    // Backing fields
    private IsPositionNotSoftRemovedSpecification _isPositionNotSoftRemovedSpecification;
    private IsPositionSoftRemovedSpecification _isPositionSoftRemovedSpecification;
    private NoTrackingOnPositionSpecification _noTrackingOnPositionSpecification;
    private SelectFieldsFromPositionSpecification _selectFieldsFromPositionSpecification;

    public IsPositionNotSoftRemovedSpecification IsPositionNotSoftRemovedSpecification
    {
        get
        {
            _isPositionNotSoftRemovedSpecification ??= new();

            return _isPositionNotSoftRemovedSpecification;
        }
    }

    public IsPositionSoftRemovedSpecification IsPositionSoftRemovedSpecification
    {
        get
        {
            _isPositionSoftRemovedSpecification ??= new();

            return _isPositionSoftRemovedSpecification;
        }
    }

    public NoTrackingOnPositionSpecification NoTrackingOnPositionSpecification
    {
        get
        {
            _noTrackingOnPositionSpecification ??= new();

            return _noTrackingOnPositionSpecification;
        }
    }

    public SelectFieldsFromPositionSpecification SelectFieldsFromPositionSpecification
    {
        get
        {
            _selectFieldsFromPositionSpecification ??= new();

            return _selectFieldsFromPositionSpecification;
        }
    }

    public PositionByIdSpecification PositionByIdSpecification(Guid positionId)
    {
        return new(positionId: positionId);
    }

    public PositionByNameSpecification PositionByNameSpecification(
        string positionName,
        bool isCaseSensitive = false)
    {
        return new(
            positionName: positionName,
            isCaseSensitive: isCaseSensitive);
    }
}

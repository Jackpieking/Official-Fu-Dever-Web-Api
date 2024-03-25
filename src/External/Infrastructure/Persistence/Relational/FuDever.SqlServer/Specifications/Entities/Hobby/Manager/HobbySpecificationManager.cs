using FuDever.Domain.Specifications.Entities.Hobby;
using FuDever.Domain.Specifications.Entities.Hobby.Manager;
using System;

namespace FuDever.SqlServer.Specifications.Entities.Hobby.Manager;

internal sealed class HobbySpecificationManager : IHobbySpecificationManager
{
    // Backing fields.
    private IHobbyAsNoTrackingSpecification _hobbyAsNoTrackingSpecification;
    private IHobbyNotTemporarilyRemovedSpecification _hobbyNotTemporarilyRemovedSpecification;
    private HobbyTemporarilyRemovedSpecification _hobbyTemporarilyRemovedSpecification;
    private ISelectFieldsFromHobbySpecification _selectFieldsFromHobbySpecification;
    private IUpdateFieldOfHobbySpecification _updateFieldOfHobbySpecification;
    private IHobbyNameIsNotDefaultSpecification _hobbyNameIsNotDefaultSpecification;

    public IHobbyAsNoTrackingSpecification HobbyAsNoTrackingSpecification
    {
        get
        {
            _hobbyAsNoTrackingSpecification ??= new HobbyAsNoTrackingSpecification();

            return _hobbyAsNoTrackingSpecification;
        }
    }

    public IHobbyNotTemporarilyRemovedSpecification HobbyNotTemporarilyRemovedSpecification
    {
        get
        {
            _hobbyNotTemporarilyRemovedSpecification ??= new HobbyNotTemporarilyRemovedSpecification();

            return _hobbyNotTemporarilyRemovedSpecification;
        }
    }

    public IHobbyTemporarilyRemovedSpecification HobbyTemporarilyRemovedSpecification
    {
        get
        {
            _hobbyTemporarilyRemovedSpecification ??= new HobbyTemporarilyRemovedSpecification();

            return _hobbyTemporarilyRemovedSpecification;
        }
    }

    public ISelectFieldsFromHobbySpecification SelectFieldsFromHobbySpecification
    {
        get
        {
            _selectFieldsFromHobbySpecification ??= new SelectFieldsFromHobbySpecification();

            return _selectFieldsFromHobbySpecification;
        }
    }

    public IUpdateFieldOfHobbySpecification UpdateFieldOfHobbySpecification
    {
        get
        {
            _updateFieldOfHobbySpecification ??= new UpdateFieldOfHobbySpecification();

            return _updateFieldOfHobbySpecification;
        }
    }

    public IHobbyNameIsNotDefaultSpecification HobbyNameIsNotDefaultSpecification
    {
        get
        {
            _hobbyNameIsNotDefaultSpecification ??= new HobbyNameIsNotDefaultSpecification();

            return _hobbyNameIsNotDefaultSpecification;
        }
    }

    public IHobbyByNameSpecification HobbyByNameSpecification(
        string hobbyName,
        bool isCaseSensitive)
    {
        return new HobbyByNameSpecification(
            hobbyName: hobbyName,
            isCaseSensitive: isCaseSensitive);
    }

    public IHobbyByIdSpecification HobbyByIdSpecification(Guid hobbyId)
    {
        return new HobbyByIdSpecification(hobbyId: hobbyId);
    }
}

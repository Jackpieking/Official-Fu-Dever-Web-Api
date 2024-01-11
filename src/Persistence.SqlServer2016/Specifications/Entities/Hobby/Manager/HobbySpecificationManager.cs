using System;
using Domain.Specifications.Entities.Hobby;
using Domain.Specifications.Entities.Hobby.Manager;

namespace Persistence.SqlServer2016.Specifications.Entities.Hobby.Manager;

internal sealed class HobbySpecificationManager : IHobbySpecificationManager
{
    // Backing fields.
    private IHobbyAsNoTrackingSpecification _hobbyAsNoTrackingSpecification;
    private IHobbyNotTemporarilyRemovedSpecification _hobbyNotTemporarilyRemovedSpecification;
    private HobbyTemporarilyRemovedSpecification _hobbyTemporarilyRemovedSpecification;
    private ISelectFieldsFromHobbySpecification _selectFieldsFromHobbySpecification;

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

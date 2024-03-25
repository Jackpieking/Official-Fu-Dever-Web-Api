using FuDever.Domain.Specifications.Entities.Platform;
using FuDever.Domain.Specifications.Entities.Platform.Manager;
using System;

namespace FuDever.SqlServer.Specifications.Entities.Platform.Manager;

/// <summary>
///     Represent implementation of platform specification manager.
/// </summary>
internal sealed class PlatformSpecificationManager : IPlatformSpecificationManager
{
    // Backing fields
    private IPlatformNotTemporarilyRemovedSpecification _platformNotTemporarilyRemovedSpecification;
    private IPlatformTemporarilyRemovedSpecification _platformTemporarilyRemovedSpecification;
    private IPlatformAsNoTrackingSpecification _platformAsNoTrackingSpecification;
    private ISelectFieldsFromPlatformSpecification _selectFieldsFromPlatformSpecification;
    private IUpdateFieldOfPlatformSpecification _updateFieldOfPlatformSpecification;
    private IPlatformNameIsNotDefaultSpecification _platformNameIsNotDefaultSpecification;

    public IPlatformNotTemporarilyRemovedSpecification PlatformNotTemporarilyRemovedSpecification
    {
        get
        {
            _platformNotTemporarilyRemovedSpecification ??= new PlatformNotTemporarilyRemovedSpecification();

            return _platformNotTemporarilyRemovedSpecification;
        }
    }

    public IPlatformTemporarilyRemovedSpecification PlatformTemporarilyRemovedSpecification
    {
        get
        {
            _platformTemporarilyRemovedSpecification ??= new PlatformTemporarilyRemovedSpecification();

            return _platformTemporarilyRemovedSpecification;
        }
    }

    public IPlatformAsNoTrackingSpecification PlatformAsNoTrackingSpecification
    {
        get
        {
            _platformAsNoTrackingSpecification ??= new PlatformAsNoTrackingSpecification();

            return _platformAsNoTrackingSpecification;
        }
    }

    public ISelectFieldsFromPlatformSpecification SelectFieldsFromPlatformSpecification
    {
        get
        {
            _selectFieldsFromPlatformSpecification ??= new SelectFieldsFromPlatformSpecification();

            return _selectFieldsFromPlatformSpecification;
        }
    }

    public IUpdateFieldOfPlatformSpecification UpdateFieldOfPlatformSpecification
    {
        get
        {
            _updateFieldOfPlatformSpecification ??= new UpdateFieldOfPlatformSpecification();

            return _updateFieldOfPlatformSpecification;
        }
    }

    public IPlatformNameIsNotDefaultSpecification PlatformNameIsNotDefaultSpecification
    {
        get
        {
            _platformNameIsNotDefaultSpecification ??= new PlatformNameIsNotDefaultSpecification();

            return _platformNameIsNotDefaultSpecification;
        }
    }

    public IPlatformByIdSpecification PlatformByIdSpecification(Guid platformId)
    {
        return new PlatformByIdSpecification(platformId: platformId);
    }

    public IPlatformByNameSpecification PlatformByNameSpecification(
        string platformName,
        bool isCaseSensitive)
    {
        return new PlatformByNameSpecification(
            platformName: platformName,
            isCaseSensitive: isCaseSensitive);
    }
}

using FuDeverWebApi.DataAccess.Specifications.Entites.Platform.Manager.Contracts;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Platform.Manager.Implementations;

public sealed class PlatformSpecificationManager :
    IPlatformSpecificationManager
{
    // Backing fields
    private IsPlatformNotSoftRemovedSpecification _isPlatformNotSoftRemovedSpecification;
    private IsPlatformSoftRemovedSpecification _isPlatformSoftRemovedSpecification;
    private NoTrackingOnPlatformSpecification _noTrackingOnPlatformSpecification;
    private SelectFieldsFromPlatformSpecification _selectFieldsFromPlatformSpecification;

    public IsPlatformNotSoftRemovedSpecification IsPlatformNotSoftRemovedSpecification
    {
        get
        {
            _isPlatformNotSoftRemovedSpecification ??= new();

            return _isPlatformNotSoftRemovedSpecification;
        }
    }

    public IsPlatformSoftRemovedSpecification IsPlatformSoftRemovedSpecification
    {
        get
        {
            _isPlatformSoftRemovedSpecification ??= new();

            return _isPlatformSoftRemovedSpecification;
        }
    }

    public NoTrackingOnPlatformSpecification NoTrackingOnPlatformSpecification
    {
        get
        {
            _noTrackingOnPlatformSpecification ??= new();

            return _noTrackingOnPlatformSpecification;
        }
    }

    public SelectFieldsFromPlatformSpecification SelectFieldsFromPlatformSpecification
    {
        get
        {
            _selectFieldsFromPlatformSpecification ??= new();

            return _selectFieldsFromPlatformSpecification;
        }
    }

    public PlatformByIdSpecification PlatformByIdSpecification(Guid platformId)
    {
        return new(platformId: platformId);
    }

    public PlatformByNameSpecification PlatformByNameSpecification(
        string platformName,
        bool isCaseSensitive = false)
    {
        return new(
            platformName: platformName,
            isCaseSensitive: isCaseSensitive);
    }
}

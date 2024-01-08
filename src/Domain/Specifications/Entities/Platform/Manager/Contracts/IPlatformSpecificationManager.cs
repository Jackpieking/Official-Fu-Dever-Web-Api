using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Platform.Manager.Contracts;

public interface IPlatformSpecificationManager
{
    IsPlatformNotSoftRemovedSpecification IsPlatformNotSoftRemovedSpecification { get; }

    IsPlatformSoftRemovedSpecification IsPlatformSoftRemovedSpecification { get; }

    NoTrackingOnPlatformSpecification NoTrackingOnPlatformSpecification { get; }

    SelectFieldsFromPlatformSpecification SelectFieldsFromPlatformSpecification { get; }

    PlatformByIdSpecification PlatformByIdSpecification(Guid platformId);

    PlatformByNameSpecification PlatformByNameSpecification(
        string platformName,
        bool isCaseSensitive = false);
}

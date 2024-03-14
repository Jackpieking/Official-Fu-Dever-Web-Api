using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Platform.GetAllTemporarilyRemovedPlatforms;

/// <summary>
///     Get all temporarily removed platforms response.
/// </summary>
public sealed class GetAllTemporarilyRemovedPlatformsResponse
{
    public GetAllTemporarilyRemovedPlatformsResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Platform> FoundTemporarilyRemovedPlatforms { get; init; }

    public sealed class Platform
    {
        public Guid PlatformId { get; init; }

        public string PlatformName { get; init; }

        public DateTime PlatformRemovedAt { get; init; }

        public Guid PlatformRemovedBy { get; init; }
    }
}

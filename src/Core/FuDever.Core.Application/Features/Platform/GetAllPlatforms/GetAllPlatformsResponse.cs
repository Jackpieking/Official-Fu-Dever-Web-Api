using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Platform.GetAllPlatforms;

/// <summary>
///     Get all platforms response.
/// </summary>
public sealed class GetAllPlatformsResponse
{
    public GetAllPlatformsResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Platform> FoundPlatforms { get; init; }

    public sealed class Platform
    {
        public Guid PlatformId { get; init; }

        public string PlatformName { get; init; }
    }
}

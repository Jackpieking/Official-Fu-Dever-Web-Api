using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Platform.GetAllPlatformsByPlatformName;

/// <summary>
///     Get all platforms by name response.
/// </summary>
public sealed class GetAllPlatformsByPlatformNameResponse
{
    public GetAllPlatformsByPlatformNameResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Platform> FoundPlatforms { get; init; }

    public sealed class Platform
    {
        public Guid PlatformId { get; init; }

        public string PlatformName { get; init; }
    }
}

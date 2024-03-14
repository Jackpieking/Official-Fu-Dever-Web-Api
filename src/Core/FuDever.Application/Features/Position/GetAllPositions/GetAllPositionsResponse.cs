using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Position.GetAllPositions;

/// <summary>
///     Get all position response.
/// </summary>
public sealed class GetAllPositionsResponse
{
    public GetAllPositionsResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Position> FoundPositions { get; init; }

    public sealed class Position
    {
        public Guid PositionId { get; init; }

        public string PositionName { get; init; }
    }
}

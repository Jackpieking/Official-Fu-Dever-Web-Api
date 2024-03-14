using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Position.GetAllTemporarilyRemovedPositions;

/// <summary>
///     Get all temporarily removed positions response.
/// </summary>
public sealed class GetAllTemporarilyRemovedPositionsResponse
{
    public GetAllTemporarilyRemovedPositionsResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Position> FoundTemporarilyRemovedPositions { get; init; }

    public sealed class Position
    {
        public Guid PositionId { get; init; }

        public string PositionName { get; init; }

        public DateTime PositionRemovedAt { get; init; }

        public Guid PositionRemovedBy { get; init; }
    }
}

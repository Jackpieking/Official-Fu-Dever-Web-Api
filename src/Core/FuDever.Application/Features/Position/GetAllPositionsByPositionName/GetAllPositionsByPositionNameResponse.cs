using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Position.GetAllPositionsByPositionName;

/// <summary>
///     Get all position by position name response.
/// </summary>
public sealed class GetAllPositionsByPositionNameResponse
{
    public GetAllPositionsByPositionNameResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Position> FoundPositions { get; init; }

    public sealed class Position
    {
        public Guid PositionId { get; init; }

        public string PositionName { get; init; }
    }
}

using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Major.GetAllTemporarilyRemovedMajors;

/// <summary>
///     Get all temporarily removed majors response.
/// </summary>
public sealed class GetAllTemporarilyRemovedMajorsResponse
{
    public GetAllTemporarilyRemovedMajorsResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Major> FoundTemporarilyRemovedMajors { get; init; }

    public sealed class Major
    {
        public Guid MajorId { get; init; }

        public string MajorName { get; init; }

        public DateTime MajorRemovedAt { get; init; }

        public Guid MajorRemovedBy { get; init; }
    }
}

using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Major.GetAllMajors;

/// <summary>
///     Get all majors response.
/// </summary>
public sealed class GetAllMajorsResponse
{
    public GetAllMajorsResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Major> FoundMajors { get; init; }

    public sealed class Major
    {
        public Guid MajorId { get; init; }

        public string MajorName { get; init; }
    }
}

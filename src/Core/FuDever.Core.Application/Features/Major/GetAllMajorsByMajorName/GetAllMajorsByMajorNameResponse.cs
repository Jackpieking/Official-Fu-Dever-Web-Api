using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Major.GetAllMajorsByMajorName;

/// <summary>
///     Get all majors by major name response.
/// </summary>
public sealed class GetAllMajorsByMajorNameResponse
{
    public GetAllMajorsByMajorNameResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Major> FoundMajors { get; init; }

    public sealed class Major
    {
        public Guid MajorId { get; init; }

        public string MajorName { get; init; }
    }
}

using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Hobby.GetAllHobbies;

/// <summary>
///     Get all hobbies response.
/// </summary>
public sealed class GetAllHobbiesResponse
{
    public GetAllHobbiesResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Hobby> FoundHobbies { get; init; }

    public sealed class Hobby
    {
        public Guid HobbyId { get; init; }

        public string HobbyName { get; init; }
    }
}

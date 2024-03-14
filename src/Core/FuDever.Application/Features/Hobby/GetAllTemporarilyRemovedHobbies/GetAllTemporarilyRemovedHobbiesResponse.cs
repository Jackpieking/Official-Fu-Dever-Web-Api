using System;
using System.Collections.Generic;

namespace FuDever.Application.Features.Hobby.GetAllTemporarilyRemovedHobbies;

/// <summary>
///     Get all temporarily removed hobbies response.
/// </summary>
public sealed class GetAllTemporarilyRemovedHobbiesResponse
{
    public GetAllTemporarilyRemovedHobbiesResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Hobby> FoundTemporarilyRemovedHobbies { get; init; }

    public sealed class Hobby
    {
        public Guid HobbyId { get; init; }

        public string HobbyName { get; init; }

        public DateTime HobbyRemovedAt { get; init; }

        public Guid HobbyRemovedBy { get; init; }
    }
}

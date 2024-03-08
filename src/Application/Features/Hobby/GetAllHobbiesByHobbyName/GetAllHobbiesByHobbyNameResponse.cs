using System.Collections.Generic;
using System;

namespace Application.Features.Hobby.GetAllHobbiesByHobbyName;

/// <summary>
///     Response for getting all hobbies by hobby name.
/// </summary>
public sealed class GetAllHobbiesByHobbyNameResponse
{
    public GetAllHobbiesByHobbyNameResponseStatusCode StatusCode { get; init; }

    public IEnumerable<Hobby> FoundHobbies { get; init; }

    public sealed class Hobby
    {
        public Guid HobbyId { get; init; }

        public string HobbyName { get; init; }
    }
}

using FuDever.Domain.Entities.Base;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserHobbies" table.
/// </summary>
public class UserHobby : IBaseEntity
{
    internal UserHobby()
    {
    }

    public Guid UserId { get; set; }

    public Guid HobbyId { get; set; }

    // Navigation properties.
    public User User { get; set; }

    public Hobby Hobby { get; set; }
}
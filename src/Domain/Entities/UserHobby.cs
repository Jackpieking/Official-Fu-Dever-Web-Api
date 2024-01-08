using System;
using Domain.Entities.Base;

namespace Domain.Entities;

/// <summary>
///     Represent the "UserHobbies" table.
/// </summary>
public sealed class UserHobby :
    IBaseEntity
{
    /// <summary>
    ///     User id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Hobby id.
    /// </summary>
    public Guid HobbyId { get; set; }

    // Navigation properties.
    public User User { get; set; }

    public Hobby Hobby { get; set; }
}
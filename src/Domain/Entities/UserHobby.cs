using Domain.Entities.Base;
using System;

namespace Domain.Entities;

/// <summary>
///     Represent the "UserHobbies" table.
/// </summary>
public sealed class UserHobby : IBaseEntity
{
    private UserHobby() { }

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

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="hobby">
    ///     Hobby of user hobby.
    /// </param>
    /// <returns>
    ///     A new user hobby object.
    /// </returns>
    public static UserHobby Init(Hobby hobby)
    {
        return new()
        {
            Hobby = hobby
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="hobby">
    ///     Hobby of user hobby.
    /// </param>
    /// <param name="hobbyId">
    ///     Hobby id of user hobby.
    /// </param>
    /// <returns>
    ///     A new user hobby object.
    /// </returns>
    public static UserHobby Init(
        Guid hobbyId,
        Hobby hobby)
    {
        return new()
        {
            HobbyId = hobbyId,
            Hobby = hobby
        };
    }
}
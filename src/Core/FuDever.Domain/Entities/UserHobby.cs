using FuDever.Domain.Entities.Base;
using System;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "UserHobbies" table.
/// </summary>
public sealed class UserHobby : IBaseEntity
{
    private UserHobby() { }

    public Guid UserId { get; set; }

    public Guid HobbyId { get; set; }

    // Navigation properties.
    public User User { get; set; }

    public Hobby Hobby { get; set; }

    public static UserHobby InitFromDatabaseVer3(Hobby hobby)
    {
        return new()
        {
            Hobby = hobby
        };
    }

    public static UserHobby InitFromDatabaseVer1(
        Guid hobbyId,
        Hobby hobby)
    {
        return new()
        {
            HobbyId = hobbyId,
            Hobby = hobby
        };
    }

    public static UserHobby InitFromDatabaseVer2(Guid userId)
    {
        return new()
        {
            UserId = userId
        };
    }
}
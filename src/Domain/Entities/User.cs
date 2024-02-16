using Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities;

/// <summary>
///     Represent the "Users" table.
/// </summary>
public sealed class User :
    IdentityUser<Guid>,
    IBaseEntity,
    ICreatedEntity,
    IUpdatedEntity,
    ITemporarilyRemovedEntity
{
    public User()
    {
    }

    /// <summary>
    ///     User joining status id.
    /// </summary>
    public Guid UserJoiningStatusId { get; set; }

    /// <summary>
    ///     User position id.
    /// </summary>
    public Guid PositionId { get; set; }

    /// <summary>
    ///     User major id.
    /// </summary>
    public Guid MajorId { get; set; }

    /// <summary>
    ///     User department id.
    /// </summary>
    public Guid DepartmentId { get; set; }

    /// <summary>
    ///     User first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     User last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     User career.
    /// </summary>
    public string Career { get; set; }

    /// <summary>
    ///     User work places.
    /// </summary>
    public string Workplaces { get; set; }

    /// <summary>
    ///     User education places.
    /// </summary>
    public string EducationPlaces { get; set; }

    /// <summary>
    ///     User birthday.
    /// </summary>
    public DateTime BirthDay { get; set; }

    /// <summary>
    ///     User home address.
    /// </summary>
    public string HomeAddress { get; set; }

    /// <summary>
    ///     User self description.
    /// </summary>
    public string SelfDescription { get; set; }

    /// <summary>
    ///     User join date.
    /// </summary>
    public DateTime JoinDate { get; set; }

    /// <summary>
    ///     User activity points.
    /// </summary>
    public short ActivityPoints { get; set; }

    /// <summary>
    ///     User avatar url.
    /// </summary>
    public string AvatarUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime RemovedAt { get; set; }

    public Guid RemovedBy { get; set; }

    // Navigation properties.
    public Position Position { get; set; }

    public Major Major { get; set; }

    public Department Department { get; set; }

    public UserJoiningStatus UserJoiningStatus { get; set; }

    // Navigation collections.
    public IEnumerable<UserSkill> UserSkills { get; set; }

    public IEnumerable<Blog> Blogs { get; set; }

    public IEnumerable<UserHobby> UserHobbies { get; set; }

    public IEnumerable<Project> Projects { get; set; }

    public IEnumerable<BlogComment> BlogComments { get; set; }

    public IEnumerable<UserPlatform> UserPlatforms { get; set; }

    public IEnumerable<RefreshToken> RefreshTokens { get; set; }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="username">
    ///     Username of user account.
    /// </param>
    /// <returns>
    ///     A new user object.
    /// </returns>
    public static User InitVer1(string username)
    {
        // Validate username.
        if (string.IsNullOrWhiteSpace(value: username) ||
            username.Length > Metadata.UserName.MaxLength)
        {
            return default;
        }

        return new()
        {
            UserName = username
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="userJoiningStatus">
    ///     User joining status of user.
    /// </param>
    /// <returns>
    ///     A new user object.
    /// </returns>
    public static User InitVer2(UserJoiningStatus userJoiningStatus)
    {
        // Validate user joining status.
        if (Equals(objA: userJoiningStatus, objB: null))
        {
            return default;
        }

        return new()
        {
            UserJoiningStatus = userJoiningStatus
        };
    }

    /// <summary>
    ///     Return an instance.
    /// </summary>
    /// <param name="userId">
    ///     User id of user.
    /// </param>
    /// <param name="userFirstName">
    ///     User first name of user.
    /// </param>
    /// <param name="userLastName">
    ///     User last name of user.
    /// </param>
    /// <param name="userEmail">
    ///     User email of user.
    /// </param>
    /// <param name="userPosition">
    ///     User position of user.
    /// </param>
    /// <param name="userDepartment">
    ///     User department of user.
    /// </param>
    /// <param name="userJoiningStatus">
    ///     User joining status of user.
    /// </param>
    /// <param name="userAvatarUrl">
    ///     User avatar url of user.
    /// </param>
    /// <param name="userRemovedAt">
    ///     When is user removed.
    /// </param>
    /// <param name="userRemovedBy">
    ///     Who removed user.
    /// </param>
    /// <returns>
    ///     A new user object.
    /// </returns>
    public static User InitVer3(
        Guid userId,
        string userFirstName,
        string userLastName,
        string userEmail,
        Position userPosition,
        Department userDepartment,
        UserJoiningStatus userJoiningStatus,
        string userAvatarUrl,
        DateTime userRemovedAt,
        Guid userRemovedBy)
    {
        // Validate user id.
        if (userId.Equals(g: Guid.Empty))
        {
            return default;
        }

        // Validate user first name.
        if (string.IsNullOrWhiteSpace(value: userFirstName))
        {
            return default;
        }

        // Validate user last name.
        if (string.IsNullOrWhiteSpace(value: userLastName))
        {
            return default;
        }

        // Validate user email.
        if (string.IsNullOrWhiteSpace(value: userEmail))
        {
            return default;
        }

        // Validate user position.
        if (Equals(objA: userPosition, objB: default))
        {
            return default;
        }

        // Validate user department.
        if (Equals(objA: userDepartment, objB: default))
        {
            return default;
        }

        // Validate user joining status.
        if (Equals(objA: userJoiningStatus, objB: default))
        {
            return default;
        }

        // Validate user avatar.
        if (string.IsNullOrWhiteSpace(value: userAvatarUrl))
        {
            return default;
        }

        // Validate user removed by.
        if (userRemovedBy == Guid.Empty)
        {
            return default;
        }

        return new()
        {
            Id = userId,
            FirstName = userFirstName,
            LastName = userLastName,
            Email = userEmail,
            Position = userPosition,
            Department = userDepartment,
            UserJoiningStatus = userJoiningStatus,
            AvatarUrl = userAvatarUrl,
            RemovedAt = userRemovedAt,
            RemovedBy = userRemovedBy
        };
    }

    public static User InitVer4(
        Guid userId,
        string userFirstName,
        string userLastName,
        string userCareer,
        DateTime userBirthday,
        string userEmail,
        string userHomeAddress,
        string userPhoneNumber,
        string userSelfDescription,
        string userJoinDate,
        string userEducationPlaces,
        Position userPosition,
        Major userMajor,
        Department userDepartment,
        string userAvatarUrl,
        IEnumerable<UserPlatform> userPlatforms,
        string userWorkplace,
        IEnumerable<Project> userProjects,
        IEnumerable<UserSkill> userSkills,
        IEnumerable<UserHobby> userHobbies)
    {
        return default;
    }


    /// <summary>
    ///     Represent metadata of property.
    /// </summary>
    public static class Metadata
    {
        /// <summary>
        ///     UserName property.
        /// </summary>
        public static class UserName
        {
            /// <summary>
            ///     Max value length.
            /// </summary>
            public const int MaxLength = 256;
        }

        /// <summary>
        ///     Password property.
        /// </summary>
        public static class Password
        {
            /// <summary>
            ///     Max value length.
            /// </summary>
            public const int MaxLength = 4;
        }
    }
}
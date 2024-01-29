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
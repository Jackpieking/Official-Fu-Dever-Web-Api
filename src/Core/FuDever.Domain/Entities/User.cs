using FuDever.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.Entities;

/// <summary>
///     Represent the "Users" table.
/// </summary>
public class User :
    IdentityUser<Guid>,
    IBaseEntity,
    ICreatedEntity,
    IUpdatedEntity,
    ITemporarilyRemovedEntity
{
    internal User()
    {
    }

    public Guid UserJoiningStatusId { get; set; }

    public Guid PositionId { get; set; }

    public Guid MajorId { get; set; }

    public Guid DepartmentId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Career { get; set; }

    public string Workplaces { get; set; }

    public string EducationPlaces { get; set; }

    public DateTime BirthDay { get; set; }

    public string HomeAddress { get; set; }

    public string SelfDescription { get; set; }

    public DateTime JoinDate { get; set; }

    public short ActivityPoints { get; set; }

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

    public IEnumerable<UserRole> UserRoles { get; set; }

    public IEnumerable<UserClaim> UserClaims { get; set; }

    public IEnumerable<UserLogin> UserLogins { get; set; }

    public IEnumerable<UserToken> UserTokens { get; set; }

    public static class Metadata
    {
        public static class ActitvityPoints
        {
            public const int MinValue = 0;

            public const int MaxValue = 100;
        }

        public static class UserName
        {
            public const int MaxLength = 256;

            public const int MinLength = 2;
        }

        public static class Password
        {
            public const int MinLength = 4;

            public const int MaxLength = 256;
        }
    }
}
using FuDever.Domain.Entities;
using FuDever.Domain.EntityBuilders.User.Others;
using System;
using System.Collections.Generic;

namespace FuDever.Domain.EntityBuilders.User;

/// <summary>
///     User for database retrieving builder.
/// </summary>
public sealed class UserForDatabaseRetrievingBuilder :
    Entities.User,
    IBaseUserBuilder,
    IUserBuilder<UserForDatabaseRetrievingBuilder>,
    IUserNavigationPropertyBuilder<UserForDatabaseRetrievingBuilder>,
    IUserNavigationCollectionBuilder<UserForDatabaseRetrievingBuilder>
{
    public UserForDatabaseRetrievingBuilder WithAccessFailedCount(int userAccessFailedCount)
    {
        AccessFailedCount = userAccessFailedCount;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithActivityPoints(short userActivityPoints)
    {
        ActivityPoints = userActivityPoints;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithAvatarUrl(string userAvatarUrl)
    {
        AvatarUrl = userAvatarUrl;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithBirthDay(DateTime userBirthDay)
    {
        BirthDay = userBirthDay;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithCareer(string userCareer)
    {
        Career = userCareer;

        return this;
    }

    public Entities.User Complete()
    {
        return new()
        {
            AccessFailedCount = AccessFailedCount,
            ActivityPoints = ActivityPoints,
            AvatarUrl = AvatarUrl,
            BirthDay = BirthDay,
            Career = Career,
            ConcurrencyStamp = ConcurrencyStamp,
            CreatedAt = CreatedAt,
            CreatedBy = CreatedBy,
            DepartmentId = DepartmentId,
            EducationPlaces = EducationPlaces,
            FirstName = FirstName,
            HomeAddress = HomeAddress,
            JoinDate = JoinDate,
            LastName = LastName,
            LockoutEnd = LockoutEnd,
            LockoutEnabled = LockoutEnabled,
            MajorId = MajorId,
            PasswordHash = PasswordHash,
            PhoneNumber = PhoneNumber,
            PhoneNumberConfirmed = PhoneNumberConfirmed,
            PositionId = PositionId,
            RemovedAt = RemovedAt,
            RemovedBy = RemovedBy,
            SecurityStamp = SecurityStamp,
            SelfDescription = SelfDescription,
            TwoFactorEnabled = TwoFactorEnabled,
            UpdatedAt = UpdatedAt,
            UpdatedBy = UpdatedBy,
            UserJoiningStatusId = UserJoiningStatusId,
            Email = Email,
            EmailConfirmed = EmailConfirmed,
            Id = Id,
            NormalizedUserName = NormalizedUserName,
            NormalizedEmail = NormalizedEmail,
            UserName = UserName,
            Workplaces = Workplaces,
            Position = Position,
            Major = Major,
            Department = Department,
            UserJoiningStatus = UserJoiningStatus,
            UserSkills = UserSkills,
            Blogs = Blogs,
            UserHobbies = UserHobbies,
            Projects = Projects,
            BlogComments = BlogComments,
            UserPlatforms = UserPlatforms,
            RefreshTokens = RefreshTokens,
            UserRoles = UserRoles,
            UserClaims = UserClaims,
            UserLogins = UserLogins,
            UserTokens = UserTokens,
        };
    }

    public UserForDatabaseRetrievingBuilder WithConcurrencyStamp(string userConcurrencyStamp)
    {
        ConcurrencyStamp = userConcurrencyStamp;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithCreatedAt(DateTime userCreatedAt)
    {
        CreatedAt = userCreatedAt;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithCreatedBy(Guid userCreatedBy)
    {
        CreatedBy = userCreatedBy;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithDepartmentId(Guid userDepartmentId)
    {
        DepartmentId = userDepartmentId;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithEducationPlaces(string userEducationPlaces)
    {
        EducationPlaces = userEducationPlaces;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithFirstName(string userFirstName)
    {
        FirstName = userFirstName;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithHomeAddress(string userHomeAddress)
    {
        HomeAddress = userHomeAddress;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithJoinDate(DateTime userJoinDate)
    {
        JoinDate = userJoinDate;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithLastName(string userLastName)
    {
        LastName = userLastName;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithLockoutEnabled(bool userLockoutEnabled)
    {
        LockoutEnabled = userLockoutEnabled;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithLockoutEnd(DateTimeOffset userLockoutEnd)
    {
        LockoutEnd = userLockoutEnd;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithMajorId(Guid userMajorId)
    {
        MajorId = userMajorId;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithPasswordHash(string userPasswordHash)
    {
        PasswordHash = userPasswordHash;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithPhoneNumber(string userPhoneNumber)
    {
        PhoneNumber = userPhoneNumber;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithPhoneNumberConfirmed(bool userPhoneNumberConfirmed)
    {
        PhoneNumberConfirmed = userPhoneNumberConfirmed;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithPositionId(Guid userPositionId)
    {
        PositionId = userPositionId;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithRemovedAt(DateTime userRemovedAt)
    {
        RemovedAt = userRemovedAt;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithRemovedBy(Guid userRemovedBy)
    {
        RemovedBy = userRemovedBy;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithSecurityStamp(string userSecurityStamp)
    {
        SecurityStamp = userSecurityStamp;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithSelfDescription(string userSelfDescription)
    {
        SelfDescription = userSelfDescription;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithTwoFactorEnabled(bool userTwoFactorEnabled)
    {
        TwoFactorEnabled = userTwoFactorEnabled;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUpdatedAt(DateTime userUpdatedAt)
    {
        UpdatedAt = userUpdatedAt;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUpdatedBy(Guid userUpdatedBy)
    {
        UpdatedBy = userUpdatedBy;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUserJoiningStatusId(Guid userJoiningStatusId)
    {
        UserJoiningStatusId = userJoiningStatusId;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithEmail(string userEmail)
    {
        Email = userEmail;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithEmailConfirmed(bool userEmailConfirmed)
    {
        EmailConfirmed = userEmailConfirmed;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithId(Guid userId)
    {
        Id = userId;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithNormalizedEmail(string userNormalizedEmail)
    {
        NormalizedEmail = userNormalizedEmail;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithNormalizedUserName(string normalizedUserName)
    {
        NormalizedUserName = normalizedUserName;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUserName(string username)
    {
        UserName = username;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithWorkplaces(string userWorkplaces)
    {
        Workplaces = userWorkplaces;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithPosition(Entities.Position userPosition)
    {
        Position = userPosition;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithMajor(Entities.Major userMajor)
    {
        Major = userMajor;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithDepartment(Entities.Department userDepartment)
    {
        Department = userDepartment;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUserJoiningStatus(Entities.UserJoiningStatus userJoiningStatus)
    {
        UserJoiningStatus = userJoiningStatus;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUserSkills(IEnumerable<Entities.UserSkill> userSkills)
    {
        UserSkills = userSkills;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithBlogs(IEnumerable<Blog> userBlogs)
    {
        Blogs = userBlogs;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUserHobbies(IEnumerable<Entities.UserHobby> userHobbies)
    {
        UserHobbies = userHobbies;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithProjects(IEnumerable<Entities.Project> userProjects)
    {
        Projects = userProjects;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithBlogComments(IEnumerable<BlogComment> userBlogComments)
    {
        BlogComments = userBlogComments;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUserPlatforms(IEnumerable<Entities.UserPlatform> userPlatforms)
    {
        UserPlatforms = userPlatforms;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithRefreshTokens(IEnumerable<Entities.RefreshToken> userRefreshTokens)
    {
        RefreshTokens = userRefreshTokens;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUserRoles(IEnumerable<Entities.UserRole> userRoles)
    {
        UserRoles = userRoles;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUserClaims(IEnumerable<UserClaim> userClaims)
    {
        UserClaims = userClaims;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUserLogins(IEnumerable<UserLogin> userLogins)
    {
        UserLogins = userLogins;

        return this;
    }

    public UserForDatabaseRetrievingBuilder WithUserTokens(IEnumerable<UserToken> userTokens)
    {
        UserTokens = userTokens;

        return this;
    }
}

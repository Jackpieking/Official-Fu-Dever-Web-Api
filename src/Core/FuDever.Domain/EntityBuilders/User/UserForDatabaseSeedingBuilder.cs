using FuDever.Domain.EntityBuilders.User.Others;
using System;

namespace FuDever.Domain.EntityBuilders.User;

/// <summary>
///     User for database seeding builder.
/// </summary>
public sealed class UserForDatabaseSeedingBuilder :
    Entities.User,
    IBaseUserBuilder,
    IUserBuilder<UserForDatabaseSeedingBuilder>
{
    public UserForDatabaseSeedingBuilder WithAccessFailedCount(int userAccessFailedCount)
    {
        AccessFailedCount = userAccessFailedCount;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithActivityPoints(short userActivityPoints)
    {
        if (userActivityPoints < Entities.User.Metadata.ActitvityPoints.MinValue ||
            userActivityPoints < Entities.User.Metadata.ActitvityPoints.MaxValue)
        {
            return default;
        }

        ActivityPoints = userActivityPoints;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithAvatarUrl(string userAvatarUrl)
    {
        AvatarUrl = userAvatarUrl;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithBirthDay(DateTime userBirthDay)
    {
        if (userBirthDay == DateTime.MaxValue)
        {
            return default;
        }

        BirthDay = userBirthDay;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithCareer(string userCareer)
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

    public UserForDatabaseSeedingBuilder WithConcurrencyStamp(string userConcurrencyStamp)
    {
        ConcurrencyStamp = userConcurrencyStamp;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithCreatedAt(DateTime userCreatedAt)
    {
        if (userCreatedAt == DateTime.MaxValue)
        {
            return default;
        }

        CreatedAt = userCreatedAt;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithCreatedBy(Guid userCreatedBy)
    {
        if (userCreatedBy == Guid.Empty)
        {
            return default;
        }

        CreatedBy = userCreatedBy;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithDepartmentId(Guid userDepartmentId)
    {
        if (userDepartmentId == Guid.Empty)
        {
            return default;
        }

        DepartmentId = userDepartmentId;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithEducationPlaces(string userEducationPlaces)
    {
        EducationPlaces = userEducationPlaces;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithFirstName(string userFirstName)
    {
        FirstName = userFirstName;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithHomeAddress(string userHomeAddress)
    {
        HomeAddress = userHomeAddress;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithJoinDate(DateTime userJoinDate)
    {
        if (userJoinDate == DateTime.MaxValue)
        {
            return default;
        }

        JoinDate = userJoinDate;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithLastName(string userLastName)
    {
        LastName = userLastName;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithLockoutEnabled(bool userLockoutEnabled)
    {
        LockoutEnabled = userLockoutEnabled;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithLockoutEnd(DateTimeOffset userLockoutEnd)
    {
        LockoutEnd = userLockoutEnd;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithMajorId(Guid userMajorId)
    {
        if (userMajorId == Guid.Empty)
        {
            return default;
        }

        MajorId = userMajorId;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithPasswordHash(string userPasswordHash)
    {
        PasswordHash = userPasswordHash;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithPhoneNumber(string userPhoneNumber)
    {
        PhoneNumber = userPhoneNumber;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithPhoneNumberConfirmed(bool userPhoneNumberConfirmed)
    {
        PhoneNumberConfirmed = userPhoneNumberConfirmed;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithPositionId(Guid userPositionId)
    {
        if (userPositionId == Guid.Empty)
        {
            return default;
        }

        PositionId = userPositionId;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithRemovedAt(DateTime userRemovedAt)
    {
        if (userRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        RemovedAt = userRemovedAt;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithRemovedBy(Guid userRemovedBy)
    {
        if (userRemovedBy == Guid.Empty)
        {
            return default;
        }

        RemovedBy = userRemovedBy;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithSecurityStamp(string userSecurityStamp)
    {
        SecurityStamp = userSecurityStamp;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithSelfDescription(string userSelfDescription)
    {
        SelfDescription = userSelfDescription;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithTwoFactorEnabled(bool userTwoFactorEnabled)
    {
        TwoFactorEnabled = userTwoFactorEnabled;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithUpdatedAt(DateTime userUpdatedAt)
    {
        if (userUpdatedAt == DateTime.MaxValue)
        {
            return default;
        }

        UpdatedAt = userUpdatedAt;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithUpdatedBy(Guid userUpdatedBy)
    {
        if (userUpdatedBy == Guid.Empty)
        {
            return default;
        }

        UpdatedBy = userUpdatedBy;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithUserJoiningStatusId(Guid userJoiningStatusId)
    {
        if (userJoiningStatusId == Guid.Empty)
        {
            return default;
        }

        UserJoiningStatusId = userJoiningStatusId;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithEmail(string userEmail)
    {
        Email = userEmail;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithEmailConfirmed(bool userEmailConfirmed)
    {
        EmailConfirmed = userEmailConfirmed;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithId(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return default;
        }

        Id = userId;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithNormalizedEmail(string userNormalizedEmail)
    {
        NormalizedEmail = userNormalizedEmail;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithNormalizedUserName(string normalizedUserName)
    {
        NormalizedUserName = normalizedUserName;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithUserName(string username)
    {
        UserName = username;

        return this;
    }

    public UserForDatabaseSeedingBuilder WithWorkplaces(string userWorkplaces)
    {
        Workplaces = userWorkplaces;

        return this;
    }
}

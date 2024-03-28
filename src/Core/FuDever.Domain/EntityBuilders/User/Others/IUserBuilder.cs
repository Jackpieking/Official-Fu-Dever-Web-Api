using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.User.Others;

/// <summary>
///     Interface for user builder.
/// </summary>
public interface IUserBuilder<TBuilder> :
    IBaseEntityHandler<Entities.User>
        where TBuilder : IBaseUserBuilder
{
    /// <summary>
    ///     Set id of user.
    /// </summary>
    /// <param name="userId">
    ///     Id of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithId(Guid userId);

    /// <summary>
    ///     Set username of user.
    /// </summary>
    /// <param name="username">
    ///     Username of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserName(string username);

    /// <summary>
    ///     Set normalized username of user.
    /// </summary>
    /// <param name="normalizedUserName">
    ///     Normalized username of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithNormalizedUserName(string normalizedUserName);

    /// <summary>
    ///     Set email of user.
    /// </summary>
    /// <param name="userEmail">
    ///     Email of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithEmail(string userEmail);

    /// <summary>
    ///     Set normalized email of user.
    /// </summary>
    /// <param name="userNormalizedEmail">
    ///     Normalized email of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithNormalizedEmail(string userNormalizedEmail);

    /// <summary>
    ///     Set email confirmed of user.
    /// </summary>
    /// <param name="userEmailConfirmed">
    ///     Email confirmed of user.
    /// </param>
    /// <returns></returns>
    TBuilder WithEmailConfirmed(bool userEmailConfirmed);

    /// <summary>
    ///     Set password hash of user.
    /// </summary>
    /// <param name="userPasswordHash">
    ///     Password hash of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithPasswordHash(string userPasswordHash);

    /// <summary>
    ///     Set security stamp of user.
    /// </summary>
    /// <param name="userSecurityStamp">
    ///     Security stamp of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithSecurityStamp(string userSecurityStamp);

    /// <summary>
    ///     Set concurrency stamp of user.
    /// </summary>
    /// <param name="userConcurrencyStamp">
    ///     Concurrency stamp of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithConcurrencyStamp(string userConcurrencyStamp);

    /// <summary>
    ///     Set phone number of user.
    /// </summary>
    /// <param name="userPhoneNumber">
    ///     Phone number of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithPhoneNumber(string userPhoneNumber);

    /// <summary>
    ///     Set phone number confirmed of user.
    /// </summary>
    /// <param name="userPhoneNumberConfirmed">
    ///     Phone number confirmed of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithPhoneNumberConfirmed(bool userPhoneNumberConfirmed);

    /// <summary>
    ///     Set two factor enabled of user.
    /// </summary>
    /// <param name="userTwoFactorEnabled">
    ///     Two factor enabled of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithTwoFactorEnabled(bool userTwoFactorEnabled);

    /// <summary>
    ///     Set lockout end of user.
    /// </summary>
    /// <param name="userLockoutEnd">
    ///     Lockout end of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithLockoutEnd(DateTimeOffset? userLockoutEnd);

    /// <summary>
    ///     Set lockout enabled of user.
    /// </summary>
    /// <param name="userLockoutEnabled">
    ///     Lockout enabled of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithLockoutEnabled(bool userLockoutEnabled);

    /// <summary>
    ///     Set access failed count of user.
    /// </summary>
    /// <param name="userAccessFailedCount">
    ///     Access failed count of user.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithAccessFailedCount(int userAccessFailedCount);

    /// <summary>
    ///     Set user joining status id.
    /// </summary>
    /// <param name="userJoiningStatusId">
    ///     User joining status id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserJoiningStatusId(Guid userJoiningStatusId);

    /// <summary>
    ///     Set user position id.
    /// </summary>
    /// <param name="userPositionId">
    ///     User position id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithPositionId(Guid userPositionId);

    /// <summary>
    ///     Set user major id.
    /// </summary>
    /// <param name="userMajorId">
    ///     User major id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithMajorId(Guid userMajorId);

    /// <summary>
    ///     Set user department id.
    /// </summary>
    /// <param name="userDepartmentId">
    ///     User department id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithDepartmentId(Guid userDepartmentId);

    /// <summary>
    ///     Set user first name.
    /// </summary>
    /// <param name="userFirstName">
    ///     User first name.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithFirstName(string userFirstName);

    /// <summary>
    ///     Set user last name.
    /// </summary>
    /// <param name="userLastName">
    ///     User last name.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithLastName(string userLastName);

    /// <summary>
    ///     Set user career.
    /// </summary>
    /// <param name="userCareer">
    ///     User career.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithCareer(string userCareer);

    /// <summary>
    ///     Set user workplaces.
    /// </summary>
    /// <param name="userWorkplaces">
    ///     User workplace.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithWorkplaces(string userWorkplaces);

    /// <summary>
    ///     Set user education places.
    /// </summary>
    /// <param name="userEducationPlaces">
    ///     User education places.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithEducationPlaces(string userEducationPlaces);

    /// <summary>
    ///     Set user birth day.
    /// </summary>
    /// <param name="userBirthDay">
    ///     User birth day.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithBirthDay(DateTime userBirthDay);

    /// <summary>
    ///     Set user home address.
    /// </summary>
    /// <param name="userHomeAddress">
    ///     User home address.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithHomeAddress(string userHomeAddress);

    /// <summary>
    ///     Set user self description.
    /// </summary>
    /// <param name="userSelfDescription">
    ///     User self description.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithSelfDescription(string userSelfDescription);

    /// <summary>
    ///     Set user join date.
    /// </summary>
    /// <param name="userJoinDate">
    ///     User join date.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithJoinDate(DateTime userJoinDate);

    /// <summary>
    ///     Set user activity points.
    /// </summary>
    /// <param name="userActivityPoints">
    ///     User activity points.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithActivityPoints(short userActivityPoints);

    /// <summary>
    ///     Set user avatar url.
    /// </summary>
    /// <param name="userAvatarUrl">
    ///     User avatar url.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithAvatarUrl(string userAvatarUrl);

    /// <summary>
    ///     Set user created time.
    /// </summary>
    /// <param name="userCreatedAt">
    ///     User created time.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithCreatedAt(DateTime userCreatedAt);

    /// <summary>
    ///     Set user creator id.
    /// </summary>
    /// <param name="userCreatedBy">
    ///     User creator id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithCreatedBy(Guid userCreatedBy);

    /// <summary>
    ///     Set user updated time.
    /// </summary>
    /// <param name="userUpdatedAt">
    ///     User updated time..
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUpdatedAt(DateTime userUpdatedAt);

    /// <summary>
    ///     Set user updator id.
    /// </summary>
    /// <param name="userUpdatedBy">
    ///     User updator id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUpdatedBy(Guid userUpdatedBy);

    /// <summary>
    ///     Set user removed time.
    /// </summary>
    /// <param name="userRemovedAt">
    ///     User removed time.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedAt(DateTime userRemovedAt);

    /// <summary>
    ///     Set user remover id.
    /// </summary>
    /// <param name="userRemovedBy">
    ///     User remover id.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRemovedBy(Guid userRemovedBy);
}

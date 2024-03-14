using System;

namespace FuDever.Domain.Specifications.Entities.User.Manager;

/// <summary>
///     Represent user specification manager.
/// </summary>
public interface IUserSpecificationManager
{
    /// <summary>
    ///     User not temporarily removed specification.
    /// </summary>
    IUserNotTemporarilyRemovedSpecification UserNotTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     User temporarily removed specification.
    /// </summary>
    IUserTemporarilyRemovedSpecification UserTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     User as no tracking specification.
    /// </summary>
    IUserAsNoTrackingSpecification UserAsNoTrackingSpecification { get; }

    /// <summary>
    ///     User as split query specification.
    /// </summary>
    IUserAsSplitQuerySpecification UserAsSplitQuerySpecification { get; }

    /// <summary>
    ///     Select field from "Users" table specification.
    /// </summary>
    ISelectFieldsFromUserSpecification SelectFieldsFromUserSpecification { get; }

    /// <summary>
    ///     User by user email specification.
    /// </summary>
    /// <param name="email">
    ///     Email for finding user.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserByEmailSpecification UserByEmailSpecification(string email);

    /// <summary>
    ///     User by user phone number specification.
    /// </summary>
    /// <param name="phoneNumber">
    ///     Phone number for finding user.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserByPhoneNumberSpecification UserByPhoneNumberSpecification(string phoneNumber);

    /// <summary>
    ///     User by user id specification.
    /// </summary>
    /// <param name="userId">
    ///     User id for finding user.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserByIdSpecification UserByIdSpecification(Guid userId);

    /// <summary>
    ///     User that is different from user id specification.
    /// </summary>
    /// <param name="userId">
    ///     User id to find user that is different from this id.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserNotByIdSpecification UserNotByIdSpecification(Guid userId);

    /// <summary>
    ///     User by username specification.
    /// </summary>
    /// <param name="username">
    ///     Username to find user.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserByUsernameSpecification UserByUsernameSpecification(string username);

    /// <summary>
    ///     User by position id specification.
    /// </summary>
    /// <param name="positionId">
    ///     Position id to find user.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserByPositionIdSpecification UserByPositionIdSpecification(Guid positionId);

    /// <summary>
    ///     User by major id specification.
    /// </summary>
    /// <param name="majorId">
    ///     Major id to find user.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserByMajorIdSpecification UserByMajorIdSpecification(Guid majorId);

    /// <summary>
    ///     User by department id specification.
    /// </summary>
    /// <param name="departmentId">
    ///     Department id to find user.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserByDepartmentIdSpecification UserByDepartmentIdSpecification(Guid departmentId);
}

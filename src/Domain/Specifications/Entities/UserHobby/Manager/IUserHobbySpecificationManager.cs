using System;

namespace Domain.Specifications.Entities.UserHobby.Manager;

/// <summary>
///     Represent user hobby specification manager.
/// </summary>
public interface IUserHobbySpecificationManager
{
    /// <summary>
    ///     Select field from "UserHobbies" table specification.
    /// </summary>
    ISelectFieldsFromUserHobbySpecification SelectFieldsFromUserHobbySpecification { get; }

    /// <summary>
    ///     User hobby by user id specification.
    /// </summary>
    /// <param name="userId">
    ///     User id for finding user hobby.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserHobbyByUserIdSpecification UserHobbyByUserIdSpecification(Guid userId);

    /// <summary>
    ///     User hobby as no tracking specification.
    /// </summary>
    IUserHobbyAsNoTrackingSpecification UserHobbyAsNoTrackingSpecification { get; }

    /// <summary>
    ///     User hobby by hobby id specification.
    /// </summary>
    /// <param name="hobbyId">
    ///     Hobby id for finding user hobby.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserHobbyByHobbyIdSpecification UserHobbyByHobbyIdSpecification(Guid hobbyId);
}

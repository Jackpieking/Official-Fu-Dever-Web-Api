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
}

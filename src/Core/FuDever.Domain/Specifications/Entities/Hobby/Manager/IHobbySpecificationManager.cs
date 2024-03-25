using System;

namespace FuDever.Domain.Specifications.Entities.Hobby.Manager;

/// <summary>
///     Represent hobby specification manager.
/// </summary>
public interface IHobbySpecificationManager
{
    /// <summary>
    ///     Hobby as no tracking specification.
    /// </summary>
    IHobbyAsNoTrackingSpecification HobbyAsNoTrackingSpecification { get; }

    /// <summary>
    ///     Hobby not temporarily removed specification.
    /// </summary>
    IHobbyNotTemporarilyRemovedSpecification HobbyNotTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Hobby temporarily removed specification.
    /// </summary>
    IHobbyTemporarilyRemovedSpecification HobbyTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Update field of hobby specification.
    /// </summary>
    IUpdateFieldOfHobbySpecification UpdateFieldOfHobbySpecification { get; }

    /// <summary>
    ///     Hobby by hobby id specification.
    /// </summary>
    /// <param name="hobbyId">
    ///     Hobby id for finding hobby.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IHobbyByIdSpecification HobbyByIdSpecification(Guid hobbyId);

    /// <summary>
    ///     Hobby by hobby name specification.
    /// </summary>
    /// <param name="hobbyName">
    ///     Hobby name for finding hobby.
    /// </param>
    /// <param name="isCaseSensitive">
    ///     Does hobby name need searching in a sensitive way.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IHobbyByNameSpecification HobbyByNameSpecification(
        string hobbyName,
        bool isCaseSensitive);

    /// <summary>
    ///     Select field from "Hobbies" table specification.
    /// </summary>
    ISelectFieldsFromHobbySpecification SelectFieldsFromHobbySpecification { get; }

    /// <summary>
    ///     Hobby name is not default specification.
    /// </summary>
    IHobbyNameIsNotDefaultSpecification HobbyNameIsNotDefaultSpecification { get; }
}

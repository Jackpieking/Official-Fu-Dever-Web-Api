using System;

namespace Domain.Specifications.Entities.Hobby.Manager;

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
    ///     Hobby by id specification.
    /// </summary>
    /// <param name="hobbyId">
    ///     Hobby id for finding hobby.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IHobbyByIdSpecification HobbyByIdSpecification(Guid hobbyId);

    /// <summary>
    ///     Hobby by name specification.
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
    ///     Select field from hobby table specification.
    /// </summary>
    ISelectFieldsFromHobbySpecification SelectFieldsFromHobbySpecification { get; }
}

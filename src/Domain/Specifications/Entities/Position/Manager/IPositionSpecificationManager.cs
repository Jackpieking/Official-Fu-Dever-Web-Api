using System;

namespace Domain.Specifications.Entities.Position.Manager;

/// <summary>
///     Represent position specification manager.
/// </summary>
public interface IPositionSpecificationManager
{
    /// <summary>
    ///     Position not temporarily removed specification.
    /// </summary>
    IPositionNotTemporarilyRemovedSpecification PositionNotTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Position temporarily removed specification.
    /// </summary>
    IPositionTemporarilyRemovedSpecification PositionTemporarilyRemovedSpecification { get; }

    /// <summary>
    ///     Position as no tracking specification.
    /// </summary>
    IPositionAsNoTrackingSpecification PositionAsNoTrackingSpecification { get; }

    /// <summary>
    ///     Select field from "Positions" table specification.
    /// </summary>
    ISelectFieldsFromPositionSpecification SelectFieldsFromPositionSpecification { get; }

    /// <summary>
    ///     Position by position id specification.
    /// </summary>
    /// <param name="positionId">
    ///     Position id for finding position.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IPositionByIdSpecification PositionByIdSpecification(Guid positionId);

    /// <summary>
    ///     Position by position name specification.
    /// </summary>
    /// <param name="positionName">
    ///     Position name for finding position.
    /// </param>
    /// <param name="isCaseSensitive">
    ///     Does position name need searching in a sensitive way.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IPositionByNameSpecification PositionByNameSpecification(
        string positionName,
        bool isCaseSensitive);
}

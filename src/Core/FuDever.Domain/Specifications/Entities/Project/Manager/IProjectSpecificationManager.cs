using System;

namespace FuDever.Domain.Specifications.Entities.Project.Manager;

/// <summary>
///     Represent project specification manager.
/// </summary>
public interface IProjectSpecificationManager
{
    /// <summary>
    ///     Project as no tracking specification.
    /// </summary>
    IProjectAsNoTrackingSpecification ProjectAsNoTrackingSpecification { get; }

    /// <summary>
    ///     Project by project id specification.
    /// </summary>
    /// <param name="projectId">
    ///     Project id for finding project.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IProjectByIdSpecification ProjectByIdSpecification(Guid projectId);

    /// <summary>
    ///     Project by author id specification.
    /// </summary>
    /// <param name="authorId">
    ///     Author id for finding project.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IProjectByAuthorIdSpecification ProjectByAuthorIdSpecification(Guid authorId);

    /// <summary>
    ///     Project by project title specification.
    /// </summary>
    /// <param name="projectTitle">
    ///     Project title for finding project.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IProjectByTitleSpecification ProjectByTitleSpecification(string projectTitle);

    /// <summary>
    ///     Select field from "Projects" table specification.
    /// </summary>
    ISelectFieldsFromProjectSpecification SelectFieldsFromProjectSpecification { get; }
}

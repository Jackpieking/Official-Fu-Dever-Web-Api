using FuDever.Domain.EntityBuilders.Others;
using System;

namespace FuDever.Domain.EntityBuilders.Project.Others;

/// <summary>
///     Interface for project builder.
/// </summary>
public interface IProjectBuilder<TBuilder> :
    IBaseEntityHandler<Entities.Project>
        where TBuilder : IBaseProjectBuilder
{
    /// <summary>
    ///     Set the id of project.
    /// </summary>
    /// <param name="projectId">
    ///     Id of project.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithId(Guid projectId);

    /// <summary>
    ///     Set the author id of project.
    /// </summary>
    /// <param name="projectAuthorId">
    ///     Author id of project.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithAuthorId(Guid projectAuthorId);

    /// <summary>
    ///     Set the title of project.
    /// </summary>
    /// <param name="projectTitle">
    ///     Title of project.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithTitle(string projectTitle);

    /// <summary>
    ///     Set the description of project.
    /// </summary>
    /// <param name="projectDescription">
    ///     Description of project.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithDescription(string projectDescription);

    /// <summary>
    ///     Set the source code url of project.
    /// </summary>
    /// <param name="projectSourceCodeUrl">
    ///     Source code url of project.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithSourceCodeUrl(string projectSourceCodeUrl);

    /// <summary>
    ///     Set the demo url of project.
    /// </summary>
    /// <param name="projectDemoUrl">
    ///     Demo url of project.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithDemoUrl(string projectDemoUrl);

    /// <summary>
    ///     Set the thumbnail url of project.
    /// </summary>
    /// <param name="projectThumbnailUrl">
    ///     Thumbnail url of project.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithThumbnailUrl(string projectThumbnailUrl);

    /// <summary>
    ///     Set the creator of project.
    /// </summary>
    /// <param name="projectCreatedBy">
    ///     Id of creator of project.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithCreatedBy(Guid projectCreatedBy);

    /// <summary>
    ///     Set the created time of project.
    /// </summary>
    /// <param name="projectCreatedAt">
    ///     Created time of project.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithCreatedAt(DateTime projectCreatedAt);

    /// <summary>
    ///     Set the updator of project.
    /// </summary>
    /// <param name="projectUpdatedBy">
    ///     Updator of project.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUpdatedBy(Guid projectUpdatedBy);

    /// <summary>
    ///     Set the updated time of project.
    /// </summary>
    /// <param name="projectUpdatedAt">
    ///     Updated time of project.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUpdatedAt(DateTime projectUpdatedAt);
}

using System;

namespace Domain.Specifications.Entities.Blog.Manager;

/// <summary>
///     Represent blog specification manager.
/// </summary>
public interface IBlogSpecificationManager
{
    /// <summary>
    ///     Blog by author id specification.
    /// </summary>
    /// <param name="authorId">
    ///     Author id to find blog.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IBlogByAuthorIdSpecification BlogByAuthorIdSpecification(Guid authorId);
}

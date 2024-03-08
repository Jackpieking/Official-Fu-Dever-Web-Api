using Domain.Specifications.Entities.Blog;
using Domain.Specifications.Entities.Blog.Manager;
using System;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Blog.Manager;

/// <summary>
///     Represent implementation of blog specification manager.
/// </summary>
internal sealed class BlogSpecificationManager : IBlogSpecificationManager
{
    public IBlogByAuthorIdSpecification BlogByAuthorIdSpecification(Guid authorId)
    {
        return new BlogByAuthorIdSpecification(authorId: authorId);
    }
}

using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Blog;
using System;

namespace FuDever.SqlServer.Specifications.Entities.Blog;

/// <summary>
///     Represent implementation of blog by author id specification.
/// </summary>
internal sealed class BlogByAuthorIdSpecification :
    BaseSpecification<Domain.Entities.Blog>,
    IBlogByAuthorIdSpecification
{
    internal BlogByAuthorIdSpecification(Guid authorId)
    {
        WhereExpression = blog => blog.AuthorId == authorId;
    }
}

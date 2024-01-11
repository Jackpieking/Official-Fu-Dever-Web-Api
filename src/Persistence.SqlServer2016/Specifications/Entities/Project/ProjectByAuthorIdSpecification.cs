using System;
using Domain.Specifications.Base;
using Domain.Specifications.Entities.Project;

namespace Persistence.SqlServer2016.Specifications.Entities.Project;

/// <summary>
///     Represent implementation of project by project author id specification.
/// </summary>
internal sealed class ProjectByAuthorIdSpecification :
    BaseSpecification<Domain.Entities.Project>,
    IProjectByAuthorIdSpecification
{
    internal ProjectByAuthorIdSpecification(Guid authorId)
    {
        WhereExpression = project => project.AuthorId == authorId;
    }
}

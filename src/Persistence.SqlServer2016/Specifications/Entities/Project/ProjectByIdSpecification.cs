using Domain.Specifications.Base;
using Domain.Specifications.Entities.Project;
using System;

namespace Persistence.SqlServer2016.Specifications.Entities.Project;

/// <summary>
///     Represent implementation of project by project id specification.
/// </summary>
internal sealed class ProjectByIdSpecification :
    BaseSpecification<Domain.Entities.Project>,
    IProjectByIdSpecification
{
    internal ProjectByIdSpecification(Guid projectId)
    {
        WhereExpression = project => project.Id == projectId;
    }
}

using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Project;

namespace FuDever.PostgresSql.Specifications.Entities.Project;

/// <summary>
///     Represent implementation of project by project title specification.
/// </summary>
internal sealed class ProjectByTitleSpecification :
    BaseSpecification<Domain.Entities.Project>,
    IProjectByTitleSpecification
{
    internal ProjectByTitleSpecification(string projectTitle)
    {
        WhereExpression = project => project.Title.Equals(projectTitle);
    }
}

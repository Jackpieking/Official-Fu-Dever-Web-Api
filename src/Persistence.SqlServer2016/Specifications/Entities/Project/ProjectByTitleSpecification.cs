using Domain.Specifications.Base;
using Domain.Specifications.Entities.Project;
using Microsoft.EntityFrameworkCore;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Specifications.Entities.Project;

/// <summary>
///     Represent implementation of project by project title specification.
/// </summary>
internal sealed class ProjectByTitleSpecification :
    BaseSpecification<Domain.Entities.Project>,
    IProjectByTitleSpecification
{
    internal ProjectByTitleSpecification(string projectTitle)
    {
        WhereExpression = project => EF.Functions
            .Collate(
                project.Title,
                CustomConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(projectTitle);
    }
}

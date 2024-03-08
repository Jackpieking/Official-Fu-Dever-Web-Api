using Domain.Specifications.Base;
using Domain.Specifications.Entities.Project;
using Microsoft.EntityFrameworkCore;
using Persistence.RelationalDatabase.SqlServer.Commons;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Project;

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
                CommonConstant.DbCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(projectTitle);
    }
}

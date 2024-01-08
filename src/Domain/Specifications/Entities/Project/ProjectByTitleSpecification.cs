using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Project;

public sealed class ProjectByTitleSpecification :
    GenericSpecification<ProjectEntity>
{
    public ProjectByTitleSpecification(string projectTitle)
    {
        Criteria = project => EF.Functions
            .Collate(
                project.Title,
                CustomConstants.SqlCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(projectTitle);
    }
}
